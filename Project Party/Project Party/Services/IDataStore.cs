using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace Project_Party.Services
{
    public interface IDataStore<T>
    {
        Task<bool> Connect();

        Task<IEnumerable<T>> GetItemsAsync();

        Task<IEnumerable<T>> GetItemsAsync(int count);

        Task<IEnumerable<T>> GetItemsAsync(string city);

        Task<IEnumerable<T>> GetItemsAsync(MapSpan mapSpan);

        Task<IEnumerable<T>> GetItemsAsync(Position actualPosition, double distance);
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
