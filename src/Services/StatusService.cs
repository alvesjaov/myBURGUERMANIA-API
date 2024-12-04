using myBURGUERMANIA_API.Models;
using myBURGUERMANIA_API.Data;

namespace myBURGUERMANIA_API.Services
{
    public class StatusService
    {
        private readonly ApplicationDbContext _context;

        public StatusService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Status GetStatusById(string statusId)
        {
            var status = _context.Statuses.Find(statusId);
            if (status == null)
            {
                throw new KeyNotFoundException($"Status com ID '{statusId}' não encontrado.");
            }
            return status;
        }

        public Status CreateStatus(string name)
        {
            if (_context.Statuses.Any(s => s.Name == name))
            {
                throw new ArgumentException("Já existe um status com esse nome.");
            }
            var status = new Status
            {
                Id = Guid.NewGuid().ToString(),
                Name = name
            };
            _context.Statuses.Add(status);
            _context.SaveChanges();
            return status;
        }

        public Status UpdateStatus(string id, string name)
        {
            var status = _context.Statuses.Find(id);
            if (status == null)
            {
                throw new KeyNotFoundException($"Status com ID '{id}' não encontrado.");
            }
            status.Name = name;
            _context.SaveChanges();
            return status;
        }

        public void DeleteStatus(string id)
        {
            var status = _context.Statuses.Find(id);
            if (status == null)
            {
                throw new KeyNotFoundException($"Status com ID '{id}' não encontrado.");
            }
            _context.Statuses.Remove(status);
            _context.SaveChanges();
        }

        public IEnumerable<Status> GetAll()
        {
            return _context.Statuses.ToList();
        }
    }
}
