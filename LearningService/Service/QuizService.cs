using DatabaseBroker.Repositories.Learning;
using Entity.DataTransferObjects.Learning;
using Entity.Exeptions;
using Entity.Models.Learning;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Service;

public class QuizService(
    IQuizRepository quizRepository,
    IQuestionRepository questionRepository)
    : IQuizService
{
    public async Task<QuizDto> CreateQuizAsync(QuizDto quizDto)
    {
        var quiz = new Quiz()
        {
            Duration = new TimeSpan(minutes:quizDto.durationMinutes,hours:0,seconds:0),
            TotalScore = quizDto.totalScore,
            PassingScore = quizDto.passingScore
        };

        return QuizToDto(await quizRepository.AddAsync(quiz));
    }
    public async Task<QuizDto> DeleteQuizAsync(int id)
    {
        var quiz = await quizRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Not found");

        return this.QuizToDto(await quizRepository.RemoveAsync(quiz));
    }

    public async Task<IList<QuizDto>> GetAllQuizAsync()
    {
        return await quizRepository
            .GetAllAsQueryable()
            .Select(q => this.QuizToDto(q))
            .ToListAsync();
    }

    public async Task<QuestionDto> CreateQuestionAsync(QuestionDto questionDto)
    {
        var question = new SimpleQuestion()
        {
            QuizId = questionDto.quizId,
            QuestionContent = questionDto.content,
            OrderNumber = questionDto.orderNumber,
            DocLink = questionDto.docLink,
            ImageLink = questionDto.imageLink,
            TotalBall = questionDto.ball,
            Options = questionDto.options
        };

        var simpleQuestion = await questionRepository.AddAsync(question);

        return new QuestionDto(
            simpleQuestion.Id,
            simpleQuestion.QuizId,
            simpleQuestion.OrderNumber,
            simpleQuestion.QuestionContent,
            simpleQuestion.ImageLink,
            simpleQuestion.DocLink,
            simpleQuestion.TotalBall,
            question.Options);
    }

    public async Task<QuestionDto> UpdateQuestionAsync(QuestionDto questionDto)
    {
        var question = await questionRepository
            .OfType<SimpleQuestion>()
            .OrderBy(q => q.OrderNumber)
            .FirstOrDefaultAsync(q => 
                q.Id == (int)questionDto.id!)
            ?? throw new NotFoundException("Not found");

        question.QuizId = questionDto.quizId != 0 ? questionDto.quizId : question.QuizId;
        question.QuestionContent = questionDto.content ?? question.QuestionContent;
        question.OrderNumber = questionDto.orderNumber != 0 ? questionDto.orderNumber : question.OrderNumber;
        question.DocLink = questionDto.docLink ?? question.DocLink;
        question.ImageLink = questionDto.imageLink ?? question.ImageLink;
        question.TotalBall = questionDto.ball ?? question.TotalBall;
        question.Options = questionDto.options.Count != 0 ? questionDto.options : question.Options;

        await questionRepository.UpdateAsync(question);

        return new QuestionDto(
            question.Id,
            question.QuizId,
            question.OrderNumber,
            question.QuestionContent,
            question.ImageLink,
            question.DocLink,
            question.TotalBall,
            null);
    }

    public async Task<QuestionDto> DeleteQuestionAsync(long id)
    {
        var question = await questionRepository.GetByIdAsync(id)
                   ?? throw new NotFoundException("Not found");

        var simpleQuestion = await questionRepository.RemoveAsync(question);
        
        return new QuestionDto(
            question.Id,
            question.QuizId,
            question.OrderNumber,
            question.QuestionContent,
            question.ImageLink,
            question.DocLink,
            question.TotalBall,
            null);
    }

    public async Task<(QuizDto,List<QuestionDto>)> GetQuizIncludeQuestionsByIdAsync(int id)
    {
        var quiz = await quizRepository
                       .GetAllAsQueryable()
                       .FirstOrDefaultAsync(q => q.Id == id);

        if (quiz is null)
            return (null,null);

        var questions = await questionRepository.OfType<SimpleQuestion>()
            .Where(q => q.QuizId == quiz.Id)
            .OrderBy(q => q.OrderNumber)
            .Select(question => new QuestionDto(
                question.Id,
                question.QuizId,
                question.OrderNumber,
                question.QuestionContent,
                question.ImageLink,
                question.DocLink,
                question.TotalBall,
                question.Options))
            .ToListAsync();

        return (this.QuizToDto(quiz), questions);
    }

    public async Task<QuizDto> GetQuizByIdAsync(int id)
    {
        var quiz = await quizRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Not found");

        return this.QuizToDto(quiz);
    }

    public async Task<QuizDto> UpdateQuizAsync(QuizDto quizDto)
    {
        var resultQuiz = await quizRepository.GetByIdAsync((long)quizDto.id)
            ?? throw new NotFoundException("Not found");

        resultQuiz.Duration = (quizDto.durationMinutes != 0) ? new TimeSpan(0,quizDto.durationMinutes,0) : resultQuiz.Duration;
        resultQuiz.TotalScore = (quizDto.totalScore != 0) ? quizDto.totalScore : resultQuiz.TotalScore;
        resultQuiz.PassingScore = (quizDto.passingScore is not 0) ? quizDto.passingScore : resultQuiz.PassingScore;

        return this.QuizToDto(await quizRepository.UpdateAsync(resultQuiz));
    }

    private QuizDto QuizToDto(Quiz quiz)
    {
        return new QuizDto(
            quiz.Id,
            quiz.TotalScore,
            (int)quiz.PassingScore,
            quiz.Duration.Minutes);
    }
}
