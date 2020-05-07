using System;
using OzzyBank_Demo.Domain.Entities;
using OzzyBank_Demo.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace OzzyBank_Demo.Repository
{
    public class PostRepository : IPostRepository
    {
         private readonly IOzzyBankDatabase _ozzyBankDatabase;

        public PostRepository(IOzzyBankDatabase ozzyBankDatabase)
        {
            _ozzyBankDatabase = ozzyBankDatabase;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            Console.WriteLine("Test2:A");

            await using var conn = await _ozzyBankDatabase.CreateAndOpenConnection();

            var result = await conn.QueryAsync<Post>(@"SELECT ID FROM POST");

            Console.WriteLine("Test2:B");

            return result.ToList();

            

        }
    }
}
