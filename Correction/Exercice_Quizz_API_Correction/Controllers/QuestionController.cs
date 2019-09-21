using System.Collections.Generic;
using Exercice_Quizz_API.Model;
using Exercice_Quizz_API.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Exercice_Quizz_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        
        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        // GET api/question/GetAll
        [HttpGet("getAll")]
        [Produces("application/json")]
        public ActionResult<List<Question>> GetAll()
        {
            if (_questionRepository.GetAllQuestions().Count > 0)
                return Ok(_questionRepository.GetAllQuestions());
            else
                return BadRequest();
            
        }

        // GET api/question/get/5
        [HttpGet("get/{id}")]
        public ActionResult<string> GetById(int id)
        {
            if (_questionRepository.GetQuestion(id) != null)
                return Ok(_questionRepository.GetQuestion(id));
            else
                return BadRequest();
            
        }

        // POST api/question/addQuestion
        [HttpPost("addQuestion")]
        public ActionResult<List<Question>> Post([FromBody]JObject body)
        {
            int idParam = 0;
            return ExecuteRequestPostOrPut(body, idParam);
        }

        // PUT api/question/updateQuestion/5
        [HttpPut("updateQuestion/{id}")]
        public ActionResult<List<Question>> Put(int id, [FromBody]JObject body)
        {
            int idParam = id;

            if (body.HasValues
                && body.ContainsKey("QuestionIntitule")
                && body.ContainsKey("Answer")
                && _questionRepository.GetQuestion(id) != null)
                return Ok(ExecuteRequestPostOrPut(body, idParam));
            else
                return BadRequest();
        }

        // DELETE api/question/deleteQuestion/5
        [HttpDelete("deleteQuestion/{id}")]
        public ActionResult<List<Question>> Delete(int id)
        {
            if (_questionRepository.GetQuestion(id) != null)
                return Ok(_questionRepository.DeleteQuestion(id));
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
            List<Question> questions = _questionRepository.GetAllQuestions();
            foreach (var questionItem in questions)
            {
                if (questionItem.QuestionIntitule == questionEnter.QuestionIntitule)
                    isQuestionExist = true;
            }

            return isQuestionExist;
        }


        private ActionResult ExecuteRequestPostOrPut(JObject body, int idParam)
        {
            if (body.HasValues
                && body.ContainsKey("QuestionIntitule")
                && body.ContainsKey("Answer"))
            {
                Question questionEnter = MappingQuestion(idParam, body);
                bool isQuestionExist = CheckIfQuestionExist(questionEnter);

                if (isQuestionExist == false)
                    return Ok(_questionRepository.AddOrUpdateQuestion(questionEnter));
                else
                    return BadRequest("La question existe déjà");
            }
            else
                return BadRequest();
        }


        #endregion
    }
}
