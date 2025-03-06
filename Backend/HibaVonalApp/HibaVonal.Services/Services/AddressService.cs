using Hibavonal.DataContext.Entities;
using HibaVonal.DataContext;
using HibaVonal.DataContext.Dtos;
using HibaVonal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace HibaVonal.Services.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> List();
        Task Add(AddressDto address);
        Task Update(int id, AddressDto address);
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

        public async Task Add(AddressDto address)
        {
            ObjectValidatorService<AddressDto> v = new ObjectValidatorService<AddressDto>(address);
            v.IsValid();
                Address add=new Address();
                add.Street= address.Street;
                add.City= address.City;
                add.ZIP = address.ZIP;
                add.HouseNumber=address.HouseNumber;

            if (!_context.Address.Any(d => d.Equals(add)))
            {
                await _context.Address.AddAsync(add);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new AddressAlreadyExistsException();
            }
        }
        public async Task Update(int id, AddressDto address)
        {
            ObjectValidatorService<AddressDto> v = new ObjectValidatorService<AddressDto>(address);
            v.IsValid();
            if (!_context.Address.Any(a => a.Id == id))
            {
                throw new AddressWithIdNotExistsException();
            }

            Address add=_context.Address.First(a => a.Id == id);
            add.Street = address.Street;
            add.City = address.City;
            add.ZIP = address.ZIP;
            add.HouseNumber = address.HouseNumber; 

            _context.Address.Update(add);
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
