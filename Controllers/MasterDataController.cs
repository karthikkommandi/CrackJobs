using AutoMapper;
using CrackJobs.Application.DTOs;
using CrackJobs.Application.Interfaces;
using CrackJobs.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrackJobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly ICommentService _commentService;
        private readonly ICompanyService _companyService;
        private readonly ITechnologyService _technologyService;
        private readonly IRatingService _ratingService;
        private readonly IQuestionAnswerMapService _questionAnswerMapService;
        private readonly IQuestionCompanyMapService _questionCompanyMapService;
        private readonly IQuestionTechnologyMapService _questionTechnologyMapService;
        private readonly IJobRoleService _jobRoleService;
        private readonly IQuestionJobRoleMapService _questionJobRoleMapService;
        private readonly ILikeService _likeService;
        private readonly IMapper _mapper;

        public MasterDataController(
            IQuestionService questionService,
            IAnswerService answerService,
            ICommentService commentService,
            ICompanyService companyService,
            ITechnologyService technologyService,
            IRatingService ratingService,
            IQuestionAnswerMapService questionAnswerMapService,
            IQuestionCompanyMapService questionCompanyMapService,
            IQuestionTechnologyMapService questionTechnologyMapService,
            IJobRoleService jobRoleService,
            IQuestionJobRoleMapService questionJobRoleMapService,
            ILikeService likeService,
            IMapper mapper)
        {
            _questionService = questionService;
            _answerService = answerService;
            _commentService = commentService;
            _companyService = companyService;
            _technologyService = technologyService;
            _ratingService = ratingService;
            _questionAnswerMapService = questionAnswerMapService;
            _questionCompanyMapService = questionCompanyMapService;
            _questionTechnologyMapService = questionTechnologyMapService;
            _jobRoleService = jobRoleService;
            _questionJobRoleMapService = questionJobRoleMapService;
            _likeService = likeService;
            _mapper = mapper;
        }

        #region Question Endpoints
        [HttpGet("questions")]
        public async Task<ActionResult<List<QuestionDto>>> GetAllQuestions()
        {
            var questions = await _questionService.GetAllQuestionsAsync();
            return Ok(questions);
        }

        [HttpGet("questions/{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestionById(long id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null) return NotFound();
            return Ok(question);
        }

        [HttpPost("questions")]
        public async Task<ActionResult<QuestionDto>> CreateQuestion(CreateQuestionDto dto)
        {
            var question = await _questionService.CreateQuestionAsync(dto);
            return CreatedAtAction(nameof(GetQuestionById), new { id = question.Qid }, question);
        }

        [HttpPut("questions/{id}")]
        public async Task<ActionResult<QuestionDto>> UpdateQuestion(long id, UpdateQuestionDto dto)
        {
            try
            {
                var question = await _questionService.UpdateQuestionAsync(id, dto);
                return Ok(question);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("questions/{id}")]
        public async Task<IActionResult> DeleteQuestion(long id)
        {
            var result = await _questionService.DeleteQuestionAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        #endregion

        #region Answer Endpoints
        [HttpGet("answers")]
        public async Task<ActionResult<List<AnswerDto>>> GetAllAnswers()
        {
            var answers = await _answerService.GetAllAnswersAsync();
            return Ok(answers);
        }

        [HttpGet("answers/{id}")]
        public async Task<ActionResult<AnswerDto>> GetAnswerById(long id)
        {
            var answer = await _answerService.GetAnswerByIdAsync(id);
            if (answer == null) return NotFound();
            return Ok(answer);
        }

        [HttpPost("answers")]
        public async Task<ActionResult<AnswerDto>> CreateAnswer(CreateAnswerDto dto)
        {
            var answer = await _answerService.CreateAnswerAsync(dto);
            return CreatedAtAction(nameof(GetAnswerById), new { id = answer.Aid }, answer);
        }

        [HttpPut("answers/{id}")]
        public async Task<ActionResult<AnswerDto>> UpdateAnswer(long id, UpdateAnswerDto dto)
        {
            try
            {
                var answer = await _answerService.UpdateAnswerAsync(id, dto);
                return Ok(answer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("answers/{id}")]
        public async Task<IActionResult> DeleteAnswer(long id)
        {
            var result = await _answerService.DeleteAnswerAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        #endregion

        #region Comment Endpoints
        [HttpGet("comments")]
        public async Task<ActionResult<List<CommentDto>>> GetAllComments()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("comments/{id}")]
        public async Task<ActionResult<CommentDto>> GetCommentById(long id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpGet("questions/{questionId}/comments")]
        public async Task<ActionResult<List<CommentDto>>> GetCommentsByQuestion(long questionId)
        {
            var comments = await _commentService.GetByQuestionAsync(questionId);
            return Ok(comments);
        }

        [HttpPost("comments")]
        public async Task<ActionResult<CommentDto>> CreateComment(CreateCommentDto dto)
        {
            var comment = await _commentService.CreateCommentAsync(dto);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        }

        [HttpPut("comments/{id}")]
        public async Task<ActionResult<CommentDto>> UpdateComment(long id, UpdateCommentDto dto)
        {
            try
            {
                var comment = await _commentService.UpdateCommentAsync(id, dto);
                return Ok(comment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> DeleteComment(long id)
        {
            var result = await _commentService.DeleteCommentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        #endregion

        #region Company Endpoints
        [HttpGet("companies")]
        public async Task<ActionResult<List<CompanyDto>>> GetAllCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpGet("companies/{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompanyById(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null) return NotFound();
            return Ok(company);
        }

        [HttpPost("companies")]
        public async Task<ActionResult<CompanyDto>> CreateCompany(CreateCompanyDto dto)
        {
            var company = await _companyService.CreateCompanyAsync(dto);
            return CreatedAtAction(nameof(GetCompanyById), new { id = company.Id }, company);
        }

        [HttpPut("companies/{id}")]
        public async Task<ActionResult<CompanyDto>> UpdateCompany(int id, UpdateCompanyDto dto)
        {
            try
            {
                var company = await _companyService.UpdateCompanyAsync(id, dto);
                return Ok(company);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("companies/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var result = await _companyService.DeleteCompanyAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        #endregion

        #region Technology Endpoints
        [HttpGet("technologies")]
        public async Task<ActionResult<List<TechnologyDto>>> GetAllTechnologies()
        {
            var technologies = await _technologyService.GetAllTechnologiesAsync();
            return Ok(technologies);
        }

        [HttpGet("technologies/{id}")]
        public async Task<ActionResult<TechnologyDto>> GetTechnologyById(int id)
        {
            var technology = await _technologyService.GetTechnologyByIdAsync(id);
            if (technology == null) return NotFound();
            return Ok(technology);
        }

        [HttpPost("technologies")]
        public async Task<ActionResult<TechnologyDto>> CreateTechnology(CreateTechnologyDto dto)
        {
            var technology = await _technologyService.CreateTechnologyAsync(dto);
            return CreatedAtAction(nameof(GetTechnologyById), new { id = technology.TechId }, technology);
        }

        [HttpPut("technologies/{id}")]
        public async Task<ActionResult<TechnologyDto>> UpdateTechnology(int id, UpdateTechnologyDto dto)
        {
            try
            {
                var technology = await _technologyService.UpdateTechnologyAsync(id, dto);
                return Ok(technology);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("technologies/{id}")]
        public async Task<IActionResult> DeleteTechnology(int id)
        {
            var result = await _technologyService.DeleteTechnologyAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        #endregion

        #region Rating Endpoints
        [HttpGet("ratings")]
        public async Task<ActionResult<List<RatingDto>>> GetAllRatings()
        {
            var ratings = await _ratingService.GetAllRatingsAsync();
            return Ok(ratings);
        }

        [HttpGet("ratings/{id}")]
        public async Task<ActionResult<RatingDto>> GetRatingById(int id)
        {
            var rating = await _ratingService.GetRatingByIdAsync(id);
            if (rating == null) return NotFound();
            return Ok(rating);
        }

        [HttpPost("ratings")]
        public async Task<ActionResult<RatingDto>> CreateRating(CreateRatingDto dto)
        {
            var rating = await _ratingService.CreateRatingAsync(dto);
            return CreatedAtAction(nameof(GetRatingById), new { id = rating.Rid }, rating);
        }

        [HttpPut("ratings/{id}")]
        public async Task<ActionResult<RatingDto>> UpdateRating(int id, UpdateRatingDto dto)
        {
            try
            {
                var rating = await _ratingService.UpdateRatingAsync(id, dto);
                return Ok(rating);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("ratings/{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var result = await _ratingService.DeleteRatingAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        #endregion

        #region QuestionAnswerMap Endpoints
        [HttpGet("question-answer-maps")]
        public async Task<ActionResult<List<QuestionAnswerMapDto>>> GetAllQuestionAnswerMaps()
        {
            var maps = await _questionAnswerMapService.GetAllAsync();
            return Ok(maps);
        }

        [HttpGet("question-answer-maps/{id}")]
        public async Task<ActionResult<QuestionAnswerMapDto>> GetQuestionAnswerMapById(long id)
        {
            var map = await _questionAnswerMapService.GetByIdAsync(id);
            if (map == null) return NotFound();
            return Ok(map);
        }

        [HttpPost("question-answer-maps")]
        public async Task<ActionResult<QuestionAnswerMapDto>> CreateQuestionAnswerMap(CreateQuestionAnswerMapDto dto)
        {
            var map = await _questionAnswerMapService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetQuestionAnswerMapById), new { id = map.Id }, map);
        }

        [HttpPost("question-answer-maps/bulk")]
        public async Task<ActionResult<List<QuestionAnswerMapDto>>> CreateQuestionAnswerMaps(List<CreateQuestionAnswerMapDto> dtos)
        {
            var created = new List<QuestionAnswerMapDto>();
            foreach (var dto in dtos)
            {
                created.Add(await _questionAnswerMapService.CreateAsync(dto));
            }
            return Ok(created);
        }

        [HttpPut("question-answer-maps/{id}")]
        public async Task<ActionResult<QuestionAnswerMapDto>> UpdateQuestionAnswerMap(long id, UpdateQuestionAnswerMapDto dto)
        {
            try
            {
                var map = await _questionAnswerMapService.UpdateAsync(id, dto);
                return Ok(map);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("question-answer-maps/{id}")]
        public async Task<IActionResult> DeleteQuestionAnswerMap(long id)
        {
            var result = await _questionAnswerMapService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        #endregion

        #region QuestionCompanyMap Endpoints
        [HttpGet("question-company-maps")]
        public async Task<ActionResult<List<QuestionCompanyMapDto>>> GetAllQuestionCompanyMaps()
        {
            var maps = await _questionCompanyMapService.GetAllAsync();
            return Ok(maps);
        }

        [HttpGet("question-company-maps/{id}")]
        public async Task<ActionResult<QuestionCompanyMapDto>> GetQuestionCompanyMapById(long id)
        {
            var map = await _questionCompanyMapService.GetByIdAsync(id);
            if (map == null) return NotFound();
            return Ok(map);
        }

        [HttpPost("question-company-maps")]
        public async Task<ActionResult<QuestionCompanyMapDto>> CreateQuestionCompanyMap(CreateQuestionCompanyMapDto dto)
        {
            var map = await _questionCompanyMapService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetQuestionCompanyMapById), new { id = map.Id }, map);
        }

        [HttpPost("question-company-maps/bulk")]
        public async Task<ActionResult<List<QuestionCompanyMapDto>>> CreateQuestionCompanyMaps(List<CreateQuestionCompanyMapDto> dtos)
        {
            var created = new List<QuestionCompanyMapDto>();
            foreach (var dto in dtos)
            {
                created.Add(await _questionCompanyMapService.CreateAsync(dto));
            }
            return Ok(created);
        }

        [HttpPut("question-company-maps/{id}")]
        public async Task<ActionResult<QuestionCompanyMapDto>> UpdateQuestionCompanyMap(long id, UpdateQuestionCompanyMapDto dto)
        {
            try
            {
                var map = await _questionCompanyMapService.UpdateAsync(id, dto);
                return Ok(map);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("question-company-maps/{id}")]
        public async Task<IActionResult> DeleteQuestionCompanyMap(long id)
        {
            var result = await _questionCompanyMapService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        #endregion

        #region QuestionTechnologyMap Endpoints
        [HttpGet("question-technology-maps")]
        public async Task<ActionResult<List<QuestionTechnologyMapDto>>> GetAllQuestionTechnologyMaps()
        {
            var maps = await _questionTechnologyMapService.GetAllAsync();
            return Ok(maps);
        }

        [HttpGet("question-technology-maps/{id}")]
        public async Task<ActionResult<QuestionTechnologyMapDto>> GetQuestionTechnologyMapById(int id)
        {
            var map = await _questionTechnologyMapService.GetByIdAsync(id);
            if (map == null) return NotFound();
            return Ok(map);
        }

        [HttpPost("question-technology-maps")]
        public async Task<ActionResult<QuestionTechnologyMapDto>> CreateQuestionTechnologyMap(CreateQuestionTechnologyMapDto dto)
        {
            var map = await _questionTechnologyMapService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetQuestionTechnologyMapById), new { id = map.Id }, map);
        }

        [HttpPost("question-technology-maps/bulk")]
        public async Task<ActionResult<List<QuestionTechnologyMapDto>>> CreateQuestionTechnologyMaps(List<CreateQuestionTechnologyMapDto> dtos)
        {
            var created = new List<QuestionTechnologyMapDto>();
            foreach (var dto in dtos)
            {
                created.Add(await _questionTechnologyMapService.CreateAsync(dto));
            }
            return Ok(created);
        }

        [HttpPut("question-technology-maps/{id}")]
        public async Task<ActionResult<QuestionTechnologyMapDto>> UpdateQuestionTechnologyMap(int id, UpdateQuestionTechnologyMapDto dto)
        {
            try
            {
                var map = await _questionTechnologyMapService.UpdateAsync(id, dto);
                return Ok(map);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("question-technology-maps/{id}")]
        public async Task<IActionResult> DeleteQuestionTechnologyMap(int id)
        {
            var result = await _questionTechnologyMapService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        #endregion

        #region JobRole Endpoints
        [HttpGet("job-roles")]
        public async Task<ActionResult<List<JobRoleDto>>> GetAllJobRoles()
        {
            return Ok(await _jobRoleService.GetAllJobRolesAsync());
        }

        [HttpGet("job-roles/{id}")]
        public async Task<ActionResult<JobRoleDto>> GetJobRoleById(int id)
        {
            var role = await _jobRoleService.GetJobRoleByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost("job-roles")]
        public async Task<ActionResult<JobRoleDto>> CreateJobRole(CreateJobRoleDto dto)
        {
            var role = await _jobRoleService.CreateJobRoleAsync(dto);
            return CreatedAtAction(nameof(GetJobRoleById), new { id = role.RoleId }, role);
        }

        [HttpPut("job-roles/{id}")]
        public async Task<ActionResult<JobRoleDto>> UpdateJobRole(int id, UpdateJobRoleDto dto)
        {
            try
            {
                return Ok(await _jobRoleService.UpdateJobRoleAsync(id, dto));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("job-roles/{id}")]
        public async Task<IActionResult> DeleteJobRole(int id)
        {
            var result = await _jobRoleService.DeleteJobRoleAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        #endregion

        #region QuestionJobRoleMap Endpoints
        [HttpGet("question-job-role-maps")]
        public async Task<ActionResult<List<QuestionJobRoleMapDto>>> GetAllQuestionJobRoleMaps()
        {
            return Ok(await _questionJobRoleMapService.GetAllAsync());
        }

        [HttpGet("question-job-role-maps/{id}")]
        public async Task<ActionResult<QuestionJobRoleMapDto>> GetQuestionJobRoleMapById(long id)
        {
            var map = await _questionJobRoleMapService.GetByIdAsync(id);
            if (map == null) return NotFound();
            return Ok(map);
        }

        [HttpPost("question-job-role-maps")]
        public async Task<ActionResult<QuestionJobRoleMapDto>> CreateQuestionJobRoleMap(CreateQuestionJobRoleMapDto dto)
        {
            var map = await _questionJobRoleMapService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetQuestionJobRoleMapById), new { id = map.Id }, map);
        }

        [HttpPost("question-job-role-maps/bulk")]
        public async Task<ActionResult<List<QuestionJobRoleMapDto>>> CreateQuestionJobRoleMaps(List<CreateQuestionJobRoleMapDto> dtos)
        {
            var created = new List<QuestionJobRoleMapDto>();
            foreach (var dto in dtos)
            {
                created.Add(await _questionJobRoleMapService.CreateAsync(dto));
            }
            return Ok(created);
        }

        [HttpPut("question-job-role-maps/{id}")]
        public async Task<ActionResult<QuestionJobRoleMapDto>> UpdateQuestionJobRoleMap(long id, UpdateQuestionJobRoleMapDto dto)
        {
            try
            {
                return Ok(await _questionJobRoleMapService.UpdateAsync(id, dto));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("question-job-role-maps/{id}")]
        public async Task<IActionResult> DeleteQuestionJobRoleMap(long id)
        {
            var result = await _questionJobRoleMapService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        #endregion

        #region Like Endpoints
        // Toggle a like on/off for the given user + target. Returns the new state and count.
        [HttpPost("likes/toggle")]
        public async Task<ActionResult<LikeStatusDto>> ToggleLike(ToggleLikeDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserId) || string.IsNullOrWhiteSpace(dto.TargetType))
                return BadRequest("UserId and TargetType are required.");
            return Ok(await _likeService.ToggleAsync(dto));
        }

        [HttpGet("likes/status")]
        public async Task<ActionResult<LikeStatusDto>> GetLikeStatus(
            [FromQuery] string userId, [FromQuery] string targetType, [FromQuery] long targetId)
        {
            return Ok(await _likeService.GetStatusAsync(userId, targetType, targetId));
        }

        // All likes made by a user — the UI uses this to highlight already-liked items.
        [HttpGet("likes/user/{userId}")]
        public async Task<ActionResult<List<LikeDto>>> GetLikesByUser(string userId)
        {
            return Ok(await _likeService.GetByUserAsync(userId));
        }
        #endregion
    }
}
