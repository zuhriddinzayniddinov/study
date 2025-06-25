using Entity.DataTransferObjects.Learning;
using Entity.Models.Learning;

namespace LearningService.Service;

public interface IQuizService
{
    Task<QuizDto> CreateQuizAsync(QuizDto quizDto);
    Task<QuizDto> UpdateQuizAsync(QuizDto quizDto);
    Task<QuizDto> DeleteQuizAsync(int id);
    Task<QuizDto> GetQuizByIdAsync(int id);
    Task<IList<QuizDto>> GetAllQuizAsync();
    Task<QuestionDto> CreateQuestionAsync(QuestionDto questionDto);
    Task<QuestionDto> UpdateQuestionAsync(QuestionDto quiz);
    Task<QuestionDto> DeleteQuestionAsync(long id);
    Task<(QuizDto,List<QuestionDto>)> GetQuizIncludeQuestionsByIdAsync(int id);
}