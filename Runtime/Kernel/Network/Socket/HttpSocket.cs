﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Morpheus.Network
{
    public enum HttpMethod
    {
        Connect,
        Head,
        Get,
        Post,
        Put,
        Delete,
        Options,
        Trace
    }

    internal class HttpSocket : ISocket
    {
        private HttpClient client = new HttpClient();
        private readonly IResponseProducer responseProducer;
        private readonly SocketResponseHandler onResponseComplete;

        public int Id { get; private set; }

        public HttpSocket(int id, SocketHandlerConfig handlerConfig)
        {
            Id = id;
            responseProducer = handlerConfig.responseProducer;
            onResponseComplete = handlerConfig.onResponseCompleteHandler;
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public void Reset()
        {
            client.Dispose();
            client = new HttpClient();
        }

        public void ConnectAsync() { }
        public void DisconnectAsync() { }
        public void ReceiveAsync() { }

        public void SendAsync(IRequest request)
        {
            Task.Factory.StartNew(SendAsyncInteral, request);
        }

        private async void SendAsyncInteral(object state)
        {
            try
            {
                HttpRequest request = state as HttpRequest;
                HttpResponseMessage response;
                switch (request.method)
                {
                    case HttpMethod.Get:
                    {
                        response = await client.GetAsync(request.uri);
                        break;
                    }
                    case HttpMethod.Post:
                    {
                        response = await client.PostAsync(request.uri, request.content);
                        break;
                    }
                    default:
                    {
                        return;
                    }
                }

                response.EnsureSuccessStatusCode();
                onResponseComplete(this, responseProducer.Produce(response));
            }
            catch (HttpRequestException e)
            {
                DebugLogger.LogError(e.Message);
            }
            catch (TaskCanceledException e)
            {
                DebugLogger.LogError(e.Message);
            }
            catch (Exception e)
            {
                DebugLogger.LogError(e.Message);
            }
        }
    }
}