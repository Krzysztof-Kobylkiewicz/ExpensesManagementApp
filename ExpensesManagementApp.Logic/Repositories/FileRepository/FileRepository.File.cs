using ExpensesManagementApp.Database;
using Microsoft.Extensions.Logging;
using ExpensesManagementApp.Models.File;
using Core.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ExpensesManagementApp.Logic.Repositories.FileRepository
{
    public partial class FileRepository : Repository<ApplicationDbContext, Database.DbModels.File, Models.File.File, Guid>, IFileRepository
    {
        private readonly TransactionGroupRepository.ITransactionGroupRepository transactionsRepository;

        public FileRepository(ApplicationDbContext dbContext, ILogger<Database.DbModels.File> logger, TransactionGroupRepository.ITransactionGroupRepository transactionsRepository) : base(dbContext, logger) 
        {
            this.transactionsRepository = transactionsRepository;
        }

        public async Task<Models.File.File?> GetFileAsync(Guid id) => (await GetEntitiesDTOAsync<Database.DbModels.File, Models.File.File, Guid>(f => f.Where(f => f.Id == id).Include(f => f.Transactions))).SingleOrDefault();

        public async Task<IEnumerable<Models.File.File?>> GetAllFilesAsync() => await GetEntitiesDTOAsync<Database.DbModels.File, Models.File.File, Guid>(f => f.Include(f => f.Transactions));

        private async Task<FilePackage> GetTransactionGroupsAssociatedWithFile(Models.File.File? file)
        {
            var transactionGroups = await transactionsRepository.GetTransactionGroupsAsync(file?.Transactions.Where(t => t.TransactionGroupId.HasValue).Select(t => t.TransactionGroupId) ?? []);

            return new FilePackage
            {
                File = file,
                TransactionGroups = transactionGroups
                //GroupedTransactions = transactionGroups?.Select(tg => tg?.Representant).Concat(file?.Transactions.Where(t => !t.TransactionGroupId.HasValue) ?? [])
            };
        }

        public async Task<Models.File.File> UploadFileAsync(Models.File.File file) => await AddEntityDTOAsync<Database.DbModels.File, Models.File.File, Guid>(file);

        public async Task<Models.File.File> UpdateFileAsync(Models.File.File file) => await UpdateEntityDTOAsync<Database.DbModels.File, Models.File.File, Guid>(file);

        public async Task<bool> DeleteFileAsync(Guid id) => await DeleteEntityAsync<Database.DbModels.File, Models.File.File, Guid>(id);
    }
}
