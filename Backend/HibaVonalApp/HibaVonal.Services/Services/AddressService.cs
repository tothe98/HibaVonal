using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace HibaVonal.Services.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> List();
        Task Add(Address address);
        Task Update(Address address);
        Task Delete(int id);
    }
    public class AddressService : IAddressService
    {
        private readonly SQL _context;
        public AddressService(SQL context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Address>> List()
        {
            return await _context.Address.ToListAsync();
        }

        public async Task Add(Address address)
        {
            ObjectValidatorService<Address> v = new ObjectValidatorService<Address>(address);
            v.IsValid();
            if (!_context.Address.Any(d => d.Equals(address)))
            {
                await _context.Address.AddAsync(address);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new AddressAlreadyExistsException();
            }
        }
        public async Task Update(Address address)
        {
            ObjectValidatorService<Address> v = new ObjectValidatorService<Address>(address);
            v.IsValid();
            if (!_context.Address.Any(a => a.Id == address.Id))
            {
                throw new AddressWithIdNotExistsException();
            }
            _context.Address.Update(address);
            await _context.SaveChangesAsync();
        }
        
        public async Task Delete(int id)
        {
            var address = _context.Address.FirstOrDefault(a=>a.Id==id);
            if (address == null)
            {
                throw new AddressWithIdNotExistsException();
            }
            _context.Address.Remove(address);
            await _context.SaveChangesAsync();
        }   
    }
}
