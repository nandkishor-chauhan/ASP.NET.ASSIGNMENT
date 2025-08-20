using ASP.NET.HouseBrokerAPP.DAL.Repository;
using ASP.NET.HouseBrokerAPP.Models;

namespace ASP.NET.HouseBrokerAPP.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private IRepository<Property>? _categoryRepository;
        public IRepository<Property> CategoryRepository => _categoryRepository ??= new Repository<Property>(_context);


        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
