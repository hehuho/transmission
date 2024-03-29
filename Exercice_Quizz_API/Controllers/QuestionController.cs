﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exercice_Quizz_API.Model;
using Exercice_Quizz_API.Repository;
using Microsoft.AspNetCore.Hosting;
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
            throw new NotImplementedException();
        }

        // GET api/question/get/5
        [HttpGet("get/{id}")]
        public ActionResult<string> GetById(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/question/addQuestion
        [HttpPost("addQuestion")]
        public ActionResult<List<Question>> Post([FromBody]JObject body)
        {
            throw new NotImplementedException();
        }

        // PUT api/question/updateQuestion/5
        [HttpPut("updateQuestion/{id}")]
        public ActionResult<List<Question>> Put(int id, [FromBody]JObject body)
        {
            throw new NotImplementedException();
        }

        // DELETE api/question/deleteQuestion/5
        [HttpDelete("deleteQuestion/{id}")]
        public ActionResult<List<Question>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        #region Helpers

        private static Question MappingQuestion(int questionId, JObject body)
        {
            throw new NotImplementedException();
        }

        private bool CheckIfQuestionExist(Question questionEnter)
        {
            //Check if question has already exist
            throw new NotImplementedException();
        }


        private ActionResult ExecuteRequestPostOrPut(JObject body, int idParam)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
