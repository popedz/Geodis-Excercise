using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prueba
{
    public class Comment
    {
        public string _entity { get; set; }
        public int Id { get; set; }
        public string Body { get; set; }
        public User User { get; set; }
    }

    public class Blog
    {
        public string _entity { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class User
    {
        public string _entity { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        
        public List<Blog> Blogs { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {

            #region variable
            var jsonString = @"[
  {
    ""_entity"": ""User"",
    ""id"": 1,
    ""firstName"": ""Leonel"",
    ""lastName"": ""Gomez"",
    ""age"": 27,
    ""blogs"": [
      {
      ""user"": {
        ""_entity"": ""User"",
        ""id"": 1,
        ""firstName"": ""Leonel"",
        ""lastName"": ""Gomez"",
        ""age"": 27
      },
        ""_entity"": ""Blog"",
        ""id"": 231,
        ""title"": ""La programacion y tu"",
        ""body"": ""Prepared do an dissuade be so whatever steepest. Yet her beyond looked either day wished nay. By doubtful disposed do juvenile an. Now curiosity you explained immediate why behaviour. An dispatched impossible of of melancholy favourable. Our quiet not heart along scale sense timed. Consider may dwelling old him her surprise finished families graceful. Gave led past poor met fine was new."",
        ""comments"": [
          {
            ""_entity"": ""Comment"",
            ""id"": 321,
            ""body"": ""wew lad"",
            ""user"": {
              ""_entity"": ""User"",
              ""id"": 3,
              ""firstName"": ""Salvador"",
              ""lastName"": ""Briones"",
              ""age"": 22
            }
          },
          {
            ""_entity"": ""Comment"",
            ""id"": 21334,
            ""body"": ""foo bar"",
            ""user"": {
              ""_entity"": ""User"",
              ""id"": 2,
              ""firstName"": ""Diego"",
              ""lastName"": ""Meza"",
              ""age"": 22
            }
          }
        ]
      }
    ]
  },
  {
    ""_entity"": ""User"",
    ""id"": 2,
    ""firstName"": ""Diego"",
    ""lastName"": ""Meza"",
    ""age"": 27,
    ""blogs"": [
      {
        ""_entity"": ""Blog"",
        ""id"": 832,
        ""title"": ""Wisdom"",
        ""body"": ""By an outlived insisted procured improved am. Paid hill fine ten now love even leaf. Supplied feelings mr of dissuade recurred no it offering honoured. Am of of in collecting devonshire favourable excellence. Her sixteen end ashamed cottage yet reached get hearing invited. Resources ourselves sweetness ye do no perfectly. Warmly warmth six one any wisdom. Family giving is pulled beauty chatty highly no. Blessing appetite domestic did mrs judgment rendered entirely. Highly indeed had garden not."",
        ""user"": {
          ""_entity"": ""User"",
          ""id"": 2,
          ""firstName"": ""Diego"",
          ""lastName"": ""Meza"",
          ""age"": 27
        },
        ""comments"": [
          {
            ""_entity"": ""Comment"",
            ""id"": 563,
            ""body"": ""very nice"",
            ""user"": {
              ""_entity"": ""User"",
              ""id"": 1,
              ""firstName"": ""Leonel"",
              ""lastName"": ""Gomez"",
              ""age"": 27
            }
          },
          {
            ""_entity"": ""Comment"",
            ""id"": 868,
            ""body"": ""foo bar baz"",
            ""user"": {
              ""_entity"": ""User"",
              ""id"": 3,
              ""firstName"": ""Salvador"",
              ""lastName"": ""Briones"",
              ""age"": 22
            }
          }
        ]
      },
      {
        ""_entity"": ""Blog"",
        ""id"": 647,
        ""title"": ""Admiration"",
        ""body"": ""Admiration stimulated cultivated reasonable be projection possession of. Real no near room ye bred sake if some. Is arranging furnished knowledge agreeable so. Fanny as smile up small. It vulgar chatty simple months turned oh at change of. Astonished set expression solicitude way admiration. "",
        ""user"": {
          ""_entity"": ""User"",
          ""id"": 2,
          ""firstName"": ""Diego"",
          ""lastName"": ""Meza"",
          ""age"": 27
        },
        ""comments"": [
          {
            ""_entity"": ""Comment"",
            ""id"": 957,
            ""body"": ""cool story bro"",
            ""user"": {
              ""_entity"": ""User"",
              ""id"": 1,
              ""firstName"": ""Leonel"",
              ""lastName"": ""Gomez"",
              ""age"": 27
            }
          },
          {
            ""_entity"": ""Comment"",
            ""id"": 545,
            ""body"": ""step up nibba"",
            ""user"": {
              ""_entity"": ""User"",
              ""id"": 3,
              ""firstName"": ""Salvador"",
              ""lastName"": ""Briones"",
              ""age"": 22
            }
          }
        ]
      }
    ]
  },
  {
    ""_entity"": ""User"",
    ""id"": 3,
    ""firstName"": ""Salvador"",
    ""lastName"": ""Briones"",
    ""age"": 22,
    ""blogs"": []
  }
]
"
;
            #endregion

            var obj = JsonConvert.DeserializeAnonymousType(jsonString, new List<User>());

            var listUsers = obj.Select(u => 
                    new User { _entity = u._entity, Id = u.Id, FirstName = u.FirstName, LastName = u.LastName, Age = u.Age, Blogs = new List<Blog>() });

            var listBlogs = obj.SelectMany(u => u.Blogs.Select(b =>
                    new { _entity = b._entity, id = b.Id, userId = b.User.Id, title = b.Title, body = b.Body }));

            var listComments = obj.SelectMany(u => u.Blogs.SelectMany(b => b.Comments.Select(c => 
                    new { _entity = c._entity, id = c.Id, blogId = b.Id, body = c.Body, userId=c.User.Id})));

            var response = JsonConvert.SerializeObject(new { users = listUsers, blogs = listBlogs, comments = listComments });

            Console.WriteLine(response);
            Console.ReadKey();
        }
    }
}
