using AutoFixture;
using Exercice_Quizz_API_Test.RepositoryFake;
using Exercice_Quizz_API_Vierge.Client;
using Exercice_Quizz_API_Vierge.Controllers;
using Exercice_Quizz_API_Vierge.Model;
using Exercice_Quizz_API_Vierge.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Exercice_Quizz_API_Test
{
    [TestFixture]
    public class TestRequestQuestionAPI
    {
        Uri client = new Uri("http://localhost:55124");
        QuestionController _controller;
        IQuestionRepository _service;

        ApiClient testedClient;

        public TestRequestQuestionAPI()
        {
            testedClient = new ApiClient(client);
            _service = new QuestionRepositoryFake();
            _controller = new QuestionController(_service);
        }

        [Test]
        public void GetAll_WhenCalled_ReturnOkResult()
        {
            var result = _controller.GetAll();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void GetAll_WhenCalled_ListCountSuperiorThanZero()
        {
            bool success, Expected;
            List<Question> readQuestionList;
            CreateReadQuestionList(out success, out Expected, out readQuestionList);
            int count = 0;

            if (readQuestionList != null)
                count = readQuestionList.Count;

            Assert.AreNotEqual(count, 0, "La liste de questions doit contenir des éléments");
        }

        [Test]
        public void GetAll_WhenCalled_ReturnAListOfQuestion()
        {
            bool success, Expected;
            List<Question> readQuestionList;
            CreateReadQuestionList(out success, out Expected, out readQuestionList);
            int count = 0;

            if (readQuestionList != null)
                count = readQuestionList.Count;

            Assert.IsInstanceOf<List<Question>>(readQuestionList);
        }

        [Test]
        public void GetById_WhenCalled_ReturnOkResult()
        {
            bool success, Expected;
            List<Question> readQuestionList;
            CreateReadQuestionList(out success, out Expected, out readQuestionList);

            int count = readQuestionList.Count;

            List<int> itemsId = readQuestionList.Select(q => q.QuestionId).ToList();

            Random random = new Random();

            int randomIndex = random.Next(0, (itemsId.Count - 1));

            int idSelected = itemsId[randomIndex];

            var okResult = _controller.GetById(idSelected);

            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);
        }

        [Test]
        public void GetById_WhenCalled_ReturnGoodItem()
        {
            List<Question> items;
            int idSelected;
            CreateRandomindex(out items, out idSelected);

            var okResult = _controller.GetById(idSelected).Result as OkObjectResult;

            Question questionResult = okResult.Value as Question;

            Question questionToCheck = items.Where(q => q.QuestionId == questionResult.QuestionId).SingleOrDefault();

            Assert.AreEqual(questionResult.QuestionId, questionToCheck.QuestionId, "La mauvaise question est sélectionnée");
            Assert.AreEqual(questionResult.Answer, questionToCheck.Answer, "La mauvaise question est sélectionnée");
            Assert.AreEqual(questionResult.QuestionIntitule, questionToCheck.QuestionIntitule, "La mauvaise question est sélectionnée");
        }

        [Test]
        public void Post_InvalidObjectPassed_ReturnsBadRequest()
        {
            var badObject = new { Id = 3, Name = "toto" };

            JObject jObjectBadresult = JObject.Parse(JsonConvert.SerializeObject(badObject));

            var badResult = _controller.Post(jObjectBadresult).Result;

            Assert.IsInstanceOf<BadRequestResult>(badResult);

        }

        [Test]
        public void Post_GoodObjectPassed_ReturnOkResult()
        {

            Fixture fixture = new Fixture();

            JObject questionToAdd = JObject.Parse(JsonConvert.SerializeObject(fixture.Create<Question>()));

            var okResult = _controller.Post(questionToAdd).Result;

            Assert.IsInstanceOf<OkObjectResult>(okResult);

        }

        [Test]
        public void Post_GoodObjectPassed_AddItemToList()
        {
            bool success, Expected;
            List<Question> readQuestionList;
            CreateReadQuestionList(out success, out Expected, out readQuestionList);

            int countInitial = readQuestionList.Count;

            Fixture fixture = new Fixture();

            JObject questionToAdd = JObject.Parse(JsonConvert.SerializeObject(fixture.Create<Question>()));

            var okResult = _controller.Post(questionToAdd).Result as OkObjectResult;

            List<Question> questionResult = okResult.Value as List<Question>;

            int countResult = questionResult.Count;

            Assert.AreEqual(countResult, countInitial + 1, "Le nombre d'élément ajouté ne peut-être supérieur ou inférieur à un élément !");

        }

        [Test]
        public void Post_GoodObjectPassed_LastItemInListEqualToItemPassed()
        {
            Fixture fixture = new Fixture();

            Question questionToCheck = fixture.Create<Question>();

            JObject questionToAdd = JObject.Parse(JsonConvert.SerializeObject(questionToCheck));

            var okResult = _controller.Post(questionToAdd).Result as OkObjectResult;

            List<Question> questionResult = okResult.Value as List<Question>;

            Assert.AreEqual(questionResult.Last().QuestionIntitule, questionToCheck.QuestionIntitule,
                            "La dernière question de la liste de question ne correspond pas à la question entrée en paramètre !");

            Assert.AreEqual(questionResult.Last().Answer, questionToCheck.Answer,
                            "La dernière question de la liste de question ne correspond pas à la question entrée en paramètre !");

        }

        [Test]
        public void Post_QuestionAlready_ReturnBadRequest()
        {
            bool success, Expected;
            List<Question> readQuestionList;
            CreateReadQuestionList(out success, out Expected, out readQuestionList);

            List<Question> items;
            int idSelected;
            CreateRandomindex(out items, out idSelected);

            Question badObject = readQuestionList.Where(q => q.QuestionId == idSelected).SingleOrDefault();

            JObject jObjectBadresult = JObject.Parse(JsonConvert.SerializeObject(badObject));

            var badResult = _controller.Post(jObjectBadresult).Result;

            Assert.IsInstanceOf<BadRequestObjectResult>(badResult);

        }

        [Test]
        public void Put_BadObjectPassed_ReturnBadResult()
        {

            List<Question> items;
            int idSelected;
            CreateRandomindex(out items, out idSelected);

            var badObject = new { Id = 3, Name = "toto" };

            JObject jObjectBadresult = JObject.Parse(JsonConvert.SerializeObject(badObject));

            var badResult = _controller.Put(idSelected, jObjectBadresult);

            Assert.IsInstanceOf<BadRequestResult>(badResult.Result);

        }

        [Test]
        public void Put_GoodObjectPassed_ReturnOkResult()
        {
            List<Question> items;
            int idSelected;
            CreateRandomindex(out items, out idSelected);

            Fixture fixture = new Fixture();

            JObject questionToAdd = JObject.Parse(JsonConvert.SerializeObject(fixture.Create<Question>()));

            var okResult = _controller.Put(idSelected, questionToAdd);

            Assert.IsInstanceOf<OkObjectResult>(okResult.Result);

        }

        [Test]
        public void Put_GoodObjectPassed_EqualsToItemInList()
        {
            List<Question> items;
            int idSelected;
            CreateRandomindex(out items, out idSelected);

            Fixture fixture = new Fixture();

            Question questionToAdd = fixture.Create<Question>();

            JObject questionJsonToAdd = JObject.Parse(JsonConvert.SerializeObject(questionToAdd));

            var okResult = _controller.Put(idSelected, questionJsonToAdd).Result as OkObjectResult;

            var okQuestions = okResult.Value as OkObjectResult;

            List<Question> questions = okQuestions.Value as List<Question>;

            Question questionToCheck = questions.Where(q => q.QuestionId == idSelected).SingleOrDefault();

            Assert.AreEqual(questionToAdd.QuestionIntitule, questionToCheck.QuestionIntitule,
                            "L'intitulé de la question ne correspond pas à celui de la question entrée en paramètre !");

            Assert.AreEqual(questionToAdd.Answer, questionToCheck.Answer,
                            "La réponse de la question ne correspond pas à celle de la question entrée en paramètre !");

        }

        [Test]
        public void Delete_BadIdPassed_ReturnBadRequest()
        {
            bool success, Expected;
            List<Question> listOriginalRead;

            CreateReadQuestionList(out success, out Expected, out listOriginalRead);

            Random random = new Random();

            int maxQuestionId = listOriginalRead.Select(q => q.QuestionId).Max();

            int questionIdToDelete = random.Next(maxQuestionId + 1, maxQuestionId + 20);

            var badResult = _controller.Delete(questionIdToDelete).Result;

            Assert.IsInstanceOf<BadRequestObjectResult>(badResult);

        }

        [Test]
        public void Delete_GoodIdPassed_ReturnOkRequest()
        {
            bool success, Expected;
            List<Question> listOriginalRead;

            CreateReadQuestionList(out success, out Expected, out listOriginalRead);

            Random random = new Random();

            int randomIndex = random.Next(0, (listOriginalRead.Count - 1));

            int questionIdToDelete = listOriginalRead[randomIndex].QuestionId;

            var badResult = _controller.Delete(questionIdToDelete).Result;

            Assert.IsInstanceOf<OkObjectResult>(badResult);

        }

        [Test]
        public void Delete_GoodIdPassed_RemoveOneItem()
        {
            bool success, Expected;
            List<Question> listOriginalRead;

            CreateReadQuestionList(out success, out Expected, out listOriginalRead);

            int countInitial = listOriginalRead.Count;

            Random random = new Random();

            int randomIndex = random.Next(0, (listOriginalRead.Count - 1));

            int questionIdToDelete = listOriginalRead[randomIndex].QuestionId;

            var result = _controller.Delete(questionIdToDelete).Result as OkObjectResult;

            List<Question> questionResult = result.Value as List<Question>;

            Assert.AreEqual(countInitial - 1, questionResult.Count, "On ne doit supprimer qu'un seul élément !");

        }

        [Test]
        public void Delete_GoodIdPassed_RemoveGoodItem()
        {
            bool success, Expected;
            List<Question> listOriginalRead;

            CreateReadQuestionList(out success, out Expected, out listOriginalRead);

            int countInitial = listOriginalRead.Count;

            Random random = new Random();

            int randomIndex = random.Next(0, (listOriginalRead.Count - 1));

            int questionIdToDelete = listOriginalRead[randomIndex].QuestionId;

            var result = _controller.Delete(questionIdToDelete).Result as OkObjectResult;

            List<Question> questionResult = result.Value as List<Question>;

            if (questionResult.Where(q => q.QuestionId == questionIdToDelete).SingleOrDefault() != null)
                success = false;

            Assert.AreEqual(Expected, success, "Le mauvais élément dans la liste a été supprimé !");

        }

        #region Helpers

        private void CreateReadQuestionList(out bool success, out bool Expected, out List<Question> readQuestionList)
        {
            success = true;
            Expected = true;

            var result = _controller.GetAll().Result as OkObjectResult;

            if (result != null)
                readQuestionList = result.Value as List<Question>;
            else
                readQuestionList = new List<Question>();

        }


        private void CreateRandomindex(out List<Question> items, out int idSelected)
        {
            bool success, Expected;

            CreateReadQuestionList(out success, out Expected, out items);

            List<int> itemsId = new List<int>();

            foreach (var item in items)
            {
                int id = item.QuestionId;

                itemsId.Add(id);
            }

            Random random = new Random();

            int randomIndex = random.Next(0, (itemsId.Count - 1));

            idSelected = itemsId[randomIndex];
        }

        #endregion
    }
}
