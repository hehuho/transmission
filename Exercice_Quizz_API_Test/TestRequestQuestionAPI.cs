using AutoFixture;
using Exercice_Quizz_API_Vierge.Client;
using Exercice_Quizz_API_Vierge.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Exercice_Quizz_API_Test
{
    [TestClass]
    public class TestRequestQuestionAPI
    {
        Uri client = new Uri("http://localhost:55124");
        private string _path;
        
        ApiClient testedClient;

        public TestRequestQuestionAPI()
        {
            _path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\Exercice_Quizz_API_Vierge\\Json\\Questions.json";
            testedClient = new ApiClient(client);
        }

        [TestMethod]
        public void TestGetAllRequest()
        {
            bool success, Expected;
            List<Question> readQuestionList;
            CreateReadQuestionList(out success, out Expected, out readQuestionList);

            List<Question> testedQuestionsList = testedClient.GetAllQuestions().Result;

            int i = 0;

            foreach (var readQuestionListItem in readQuestionList)
            {
                if (testedQuestionsList[i].Answer != readQuestionListItem.Answer
                    || testedQuestionsList[i].QuestionId != readQuestionListItem.QuestionId
                    || testedQuestionsList[i].QuestionIntitule != readQuestionListItem.QuestionIntitule)
                    success = false;

                i++;
            }

            Assert.AreEqual(Expected, success, "Il doit y avoir un problème dans ta méthode");

        }

        [TestMethod]
        public void TestGetByIdRequest()
        {
            bool success, Expected;
            List<Question> items;

            CreateReadQuestionList(out success, out Expected, out items);

            List<int> itemsId = new List<int>();

            foreach (var item in items)
            {
                int id = item.QuestionId;

                itemsId.Add(id);
            }

            Random random = new Random();

            int randomIndex = random.Next(0, (itemsId.Count - 1));

            int idSelected = itemsId[randomIndex];

            Question testedQuestion = testedClient.GetQuestionById(idSelected).Result;

            foreach (Question q in items)
            {
                if (q.QuestionId == testedQuestion.QuestionId && q.QuestionIntitule == testedQuestion.QuestionIntitule)
                {
                    success = true;
                }
            }

            Assert.AreEqual(Expected, success, "Il doit y avoir un problème dans ta méthode");
        }

        [TestMethod]
        public void TestPostRequest()
        {
            bool success, Expected;
            List<Question> listOriginalRead;
            List<Question> listToVerify;

            CreateReadQuestionList(out success, out Expected, out listOriginalRead);

            Fixture fixture = new Fixture();

            Question questionToAdd = fixture.Create<Question>();

            List<Question> listQuestionPostResult = testedClient.AddQuestion(questionToAdd).Result;

            CreateReadQuestionList(out success, out Expected, out listToVerify);

            if (listToVerify.Count == listOriginalRead.Count + 1
                && listToVerify.Last().Answer == questionToAdd.Answer
                && listToVerify.Last().QuestionIntitule == questionToAdd.QuestionIntitule)
                success = true;
            else
                success = false;

            WriteInJsonAndReturn(_path, listOriginalRead);

            Assert.AreEqual(Expected, success, "Il doit y avoir un problème dans ta méthode");
        }

        [TestMethod]
        public void TestPutRequest()
        {
            bool success, Expected;
            List<Question> listOriginalRead;
            List<Question> listToVerify;

            CreateReadQuestionList(out success, out Expected, out listOriginalRead);

            Fixture fixture = new Fixture();

            Question questionToModify = fixture.Create<Question>();

            Random random = new Random();

            int randomIndex = random.Next(0, (listOriginalRead.Count - 1));

            int questionIdToUpdate = listOriginalRead[randomIndex].QuestionId;

            List<Question> listQuestionUpdateResult = testedClient.UpdateQuestion(questionIdToUpdate, questionToModify).Result;

            CreateReadQuestionList(out success, out Expected, out listToVerify);

            if (listOriginalRead.Count == listToVerify.Count
                && listOriginalRead[randomIndex].QuestionIntitule != listToVerify[randomIndex].QuestionIntitule
                && listOriginalRead[randomIndex].Answer != listToVerify[randomIndex].Answer)
                success = true;
            else
                success = false;

            WriteInJsonAndReturn(_path, listOriginalRead);

            Assert.AreEqual(Expected, success, "Il doit y avoir un problème dans ta méthode");

        }

        [TestMethod]
        public void TestDeleteRequest()
        {
            bool success, Expected;
            List<Question> listOriginalRead;
            List<Question> listToVerify;

            CreateReadQuestionList(out success, out Expected, out listOriginalRead);
            
            Random random = new Random();

            int randomIndex = random.Next(0, (listOriginalRead.Count - 1));

            int questionIdToDelete = listOriginalRead[randomIndex].QuestionId;

            List<Question> listQuestionUpdateResult = testedClient.DeleteQuestion(questionIdToDelete, listOriginalRead[randomIndex]).Result;

            CreateReadQuestionList(out success, out Expected, out listToVerify);

            if (listOriginalRead.Count - 1 == listToVerify.Count
                && listToVerify.Where(item => item.QuestionId == questionIdToDelete).ToList().Count == 0)
                success = true;
            else
                success = false;

            WriteInJsonAndReturn(_path, listOriginalRead);

            Assert.AreEqual(Expected, success, "Il doit y avoir un problème dans ta méthode");

        }

        #region Helpers

        private void CreateReadQuestionList(out bool success, out bool Expected, out List<Question> readQuestionList)
        {
            success = true;
            Expected = true;
            string json;

            using (StreamReader r = new StreamReader(_path))
            {
                json = r.ReadToEnd();
            }

            readQuestionList = JsonConvert.DeserializeObject<List<Question>>(json);
        }

        private static List<Question> WriteInJsonAndReturn(string path, List<Question> questions)
        {
            string json = JsonConvert.SerializeObject(questions);

            List<Question> items = new List<Question>();

            //export data to json file. 
            using (TextWriter tw = new StreamWriter(path))
            {
                tw.Write(json);
                items = JsonConvert.DeserializeObject<List<Question>>(json);
            };

            return items;
        }

        #endregion
    }
}
