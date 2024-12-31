using Cassandra;
using crud_cassandra.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crud_cassandra.Repository
{
    public class ClasseRepository
    {
 
        public List<ClasseModel> GetClasseList()
        {
            try
            {

             
                var cluster = Cluster.Builder()
                    .AddContactPoint("127.0.0.1")  
                    .WithPort(9042)
                    .Build();
                using (var connection = cluster.Connect("classe"))
                {
                    var query = connection.Prepare("SELECT * FROM aluno");
                    var rows = connection.Execute(query.Bind()).ToList();

                    List<ClasseModel> lista = new List<ClasseModel>();

                    foreach (var row in rows)
                    {
                        lista.Add(new ClasseModel
                        {
                            id = row.GetValue<int>("id"),
                            nome = row.GetValue<string>("nome"),
                            sobrenome = row.GetValue<string>("sobrenome")
                        });
                    }

                    return lista;
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar a query: {ex.Message}");
                return new List<ClasseModel>();
            }
        }

        public ClasseModel PostClasseAluno(int id,string nome,string sobrenome)
        {
            try
            {


                var cluster = Cluster.Builder()
                    .AddContactPoint("127.0.0.1")  
                    .WithPort(9042)
                    .Build();
                using (var connection = cluster.Connect("classe"))
                {
                    var query = connection.Prepare("INSERT INTO aluno (id, nome, sobrenome) VALUES (?, ?, ?)");

                    var statement = query.Bind(id, nome, sobrenome);
                    connection.Execute(statement);
                    var obj = new ClasseModel
                    {
                        id = id,
                        nome = nome,
                        sobrenome = sobrenome
                    };

                    return obj;
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar a query: {ex.Message}");
                return null;
            }
        }

        public int PutClasseAluno(int id,string nome,string sobrenome)
        {
            try
            {

                var cluster = Cluster.Builder()
                    .AddContactPoint("127.0.0.1")  
                    .WithPort(9042)
                    .Build();
                using (var connection = cluster.Connect("classe"))
                {
                    var query = connection.Prepare("UPDATE aluno SET nome = ?, sobrenome = ? WHERE id = ?");
                    var statement = query.Bind(nome, sobrenome, id);
                    connection.Execute(statement);
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: ", ex.Message);
                return 0;
            }
        }

        public int DeleteClasseAluno(int id)
        {
            var cluster = Cluster.Builder()
                    .AddContactPoint("127.0.0.1")  
                    .WithPort(9042)
                    .Build();
            using (var connection = cluster.Connect("classe"))
            {
                var query = connection.Prepare("DELETE FROM aluno WHERE id = ?");
                var statement = query.Bind(id);
                connection.Execute(statement);
                return 1;
            }
        }

    }
}
