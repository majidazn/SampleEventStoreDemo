using EventStore.ClientAPI;
using EventStore.ClientAPI.Common.Log;
using EventStore.ClientAPI.SystemData;
using EventStore.ClientAPI.UserManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SampleEventStoreDemo {
     class program {

        static string StreamId(Guid id) {
            return String.Format("BankAccount-{0}", id.ToString());
        }
 
      static  void   Main(string[] args) {

            IEventStoreConnection connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));

            //tls
            connection.ConnectAsync().Wait();



            var aggregateId = Guid.NewGuid();
            List<IEvent> eventsToRun = new List<IEvent>();

            //commands
            //Domain Logic/Model
            //Events

            eventsToRun.Add(new AccountCreatedEvent(aggregateId, "Majid Az"));
            eventsToRun.Add(new FundsDepositedEvent(aggregateId, 150));
            eventsToRun.Add(new FundsDepositedEvent(aggregateId, 100));
            eventsToRun.Add(new FundsWithdrawedEvent(aggregateId, 60));
            eventsToRun.Add(new FundsWithdrawedEvent(aggregateId, 94));
            eventsToRun.Add(new FundsDepositedEvent(aggregateId, 4));//100


            foreach (var item in eventsToRun) {
                var jsonString = JsonConvert.SerializeObject(item,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });

                var jsonPayload = Encoding.UTF8.GetBytes(jsonString);

                var eventStoreDataType = new EventData(Guid.NewGuid(), item.GetType().Name, true, jsonPayload, null);
                try {
                    connection.AppendToStreamAsync(StreamId(aggregateId), ExpectedVersion.Any, eventStoreDataType);

                }
                catch (Exception ex) {

                    throw;
                }

            }

            var results = Task.Run(() => connection.ReadStreamEventsForwardAsync(StreamId(aggregateId), StreamPosition.Start, 999
                , false));
            Task.WaitAll();
            
            var resultData = results.Result;
            var bankState = new BankAccount();
            //foreach (var evnt in resultData.Events) {
            //    var esJsonData = Encoding.UTF8.GetString(evnt.Event.Data);

            //    if (evnt.Event.EventType == nameof(AccountCreatedEvent)) {
            //        var objState = JsonConvert.DeserializeObject<AccountCreatedEvent>(esJsonData);
            //        bankState.Apply(objState);
            //    }
            //    else if(evnt.Event.EventType== nameof(FundsDepositedEvent)) {
            //        var objState = JsonConvert.DeserializeObject<FundsDepositedEvent>(esJsonData);
            //        bankState.Apply(objState);
            //    }
            //    else {
            //        var objState = JsonConvert.DeserializeObject<FundsWithdrawedEvent>(esJsonData);
            //        bankState.Apply(objState);
            //    }
            //}

            Console.ReadLine();
        }
    }


  
}
