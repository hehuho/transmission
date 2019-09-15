using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exercice_Quizz_API.Model;
using Exercice_Quizz_API.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Exercice_Quizz_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _path;
        
        public QuestionController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _path = hostingEnvironment.ContentRootPath + "/Json/Questions.json";
        }

        // GET api/question/GetAll
        [HttpGet("getAll")]
        [Produces("application/json")]
        public ActionResult GetAll()
        {
            if (QuestionRepository.GetAllQuestions(_path).Count > 0)
                return Ok(QuestionRepository.GetAllQuestions(_path));
            else
                return BadRequest();
            
        }

        // GET api/question/get/5
        [HttpGet("get/{id}")]
        public ActionResult<string> GetById(int id)
        {
            if (QuestionRepository.GetQuestion(_path, id) != null)
                return Ok(QuestionRepository.GetQuestion(_path, id));
            else
                return BadRequest();
            
        }

        // POST api/question/addQuestion
        [HttpPost("addQuestion")]
        public ActionResult Post([FromBody]JObject body)
        {
            int idParam = 0;
            return ExecuteRequestPostOrPut(body, idParam);
        }

        // PUT api/question/updateQuestion/5
        [HttpPut("updateQuestion/{id}")]
        public ActionResult Put(int id, [FromBody]JObject body)
        {
            int idParam = id;

            if (QuestionRepository.GetQuestion(_path, id) != null)
                return Ok(ExecuteRequestPostOrPut(body, idParam));
            else
                return BadRequest();
        }

        // DELETE api/question/deleteQuestion/5
        [HttpDelete("deleteQuestion/{id}")]
        public ActionResult Delete(int id)
        {
            if (QuestionRepository.GetQuestion(_path, id) != null)
                return Ok(QuestionRepository.DeleteQuestion(_path, id));
            else
                return BadRequest("La question n'existe pas ou a déjà été supprimée");
        }

        #region Helpers
        
        private static Question MappingQuestion(int questionId, JObject body)
        {
            Question questionEnter = new Question();
            questionEnter.QuestionId = questionId;
            questionEnter.QuestionIntitule = body.GetValue("QuestionIntitule").Value<string>();
            questionEnter.Answer = body.GetValue("Answer").Value<string>();
            return questionEnter;
        }

        private bool CheckIfQuestionExist(Question questionEnter)
        {
            //Check if question has already exist
            bool isQuestionExist = false;
            List<Question> questions = QuestionRepository.GetAllQuestions(_path);
            foreach (var questionItem in questions)
            {
                if (questionItem.QuestionIntitule == questionEnter.QuestionIntitule)
                    isQuestionExist = true;
            }

            return isQuestionExist;
        }


        private ActionResult ExecuteRequestPostOrPut(JObject body, int idParam)
        {
            if (body.HasValues)
            {
                Question questionEnter = MappingQuestion(idParam, body);
                bool isQuestionExist = CheckIfQuestionExist(questionEnter);

                if (isQuestionExist == false)
                    return Ok(QuestionRepository.AddOrUpdateQuestion(_path, questionEnter));
                else
                    return BadRequest("La question existe déjà");
            }
            else
                return BadRequest();
        }


        #endregion
    }
}
