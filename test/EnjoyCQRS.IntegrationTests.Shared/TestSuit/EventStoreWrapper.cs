using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnjoyCQRS.Events;
using EnjoyCQRS.EventSource;
using EnjoyCQRS.EventSource.Snapshots;
using EnjoyCQRS.EventSource.Storage;

namespace EnjoyCQRS.IntegrationTests.Shared.TestSuit
{
    internal class EventStoreWrapper : IEventStore
    {
        public EventStoreMethods CalledMethods;

        private readonly IEventStore _eventStore;

        public EventStoreWrapper(IEventStore eventStore)
        {
            _eventStore = eventStore;

            CalledMethods &= EventStoreMethods.Ctor;
        }

        public async Task SaveSnapshotAsync<TSnapshot>(TSnapshot snapshot) where TSnapshot : ISnapshot
        {
            await _eventStore.SaveSnapshotAsync(snapshot);

            CalledMethods |= EventStoreMethods.SaveSnapshotAsync;
        }

        public async Task<ISnapshot> GetLatestSnapshotByIdAsync(Guid aggregateId)
        {
            var result = await _eventStore.GetLatestSnapshotByIdAsync(aggregateId);

            CalledMethods |= EventStoreMethods.GetLatestSnapshotByIdAsync;

            return result;
        }

        public async Task<IEnumerable<ICommitedEvent>> GetEventsForwardAsync(Guid aggregateId, int version)
        {
            var result = await _eventStore.GetEventsForwardAsync(aggregateId, version);

            CalledMethods |= EventStoreMethods.GetEventsForwardAsync;

            return result;
        }

        public void Dispose()
        {
            _eventStore.Dispose();

            CalledMethods |= EventStoreMethods.Dispose;
        }

        public void BeginTransaction()
        {
            _eventStore.BeginTransaction();

            CalledMethods |= EventStoreMethods.BeginTransaction;
        }

        public async Task CommitAsync()
        {
            await _eventStore.CommitAsync();

            CalledMethods |= EventStoreMethods.CommitAsync;
        }

        public void Rollback()
        {
            _eventStore.Rollback();

            CalledMethods |= EventStoreMethods.Rollback;
        }

        public async Task<IEnumerable<ICommitedEvent>> GetAllEventsAsync(Guid id)
        {
            var result = await _eventStore.GetAllEventsAsync(id);

            CalledMethods |= EventStoreMethods.GetAllEventsAsync;

            return result;
        }

        public async Task SaveAsync(IEnumerable<ISerializedEvent> collection)
        {
            await _eventStore.SaveAsync(collection);

            CalledMethods |= EventStoreMethods.SaveAsync;
        }
    }
}