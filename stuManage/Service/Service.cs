
using Model;
using Repository;

namespace Service
{
    public class AnnounceService : BaseService<Announce>
    {
        private readonly AnnounceRepository _AnnounceRepository;
        public AnnounceService(AnnounceRepository AnnounceRepository)
        {
            base.baseRepository = AnnounceRepository;
            _AnnounceRepository = AnnounceRepository;
        }
    }
    public class CustomerService : BaseService<Customer>
    {
        private readonly CustomerRepository _CustomerRepository;
        public CustomerService(CustomerRepository CustomerRepository)
        {
            base.baseRepository = CustomerRepository;
            _CustomerRepository = CustomerRepository;
        }
    }
    public class ExamInfoService : BaseService<ExamInfo>
    {
        private readonly ExamInfoRepository _ExamInfoRepository;
        public ExamInfoService(ExamInfoRepository ExamInfoRepository)
        {
            base.baseRepository = ExamInfoRepository;
            _ExamInfoRepository = ExamInfoRepository;
        }
    }
    public class ScoreService : BaseService<Score>
    {
        private readonly ScoreRepository _ScoreRepository;
        public ScoreService(ScoreRepository scoreRepository)
        {
            base.baseRepository = scoreRepository;
            _ScoreRepository = scoreRepository;
        }
    }
    public class StuExamService : BaseService<StuExam>
    {
        private readonly StuExamRepository _StuExamRepository;
        public StuExamService(StuExamRepository StuExamRepository)
        {
            base.baseRepository = StuExamRepository;
            _StuExamRepository = StuExamRepository;
        }
    }
    public class StudentService : BaseService<Student>
    {
        private readonly StudentRepository _StudentRepository;
        public StudentService(StudentRepository StudentRepository)
        {
            base.baseRepository = StudentRepository;
            _StudentRepository = StudentRepository;
        }
    }
    public class CompInfoService : BaseService<CompInfo>
    {
        private readonly CompInfoRepository _CompInfoRepository;
        public CompInfoService(CompInfoRepository CompInfoRepository)
        {
            base.baseRepository = CompInfoRepository;
            _CompInfoRepository = CompInfoRepository;
        }
    }

    public class StuCompService : BaseService<StuComp>
    {
        private readonly StuCompRepository _StuCompRepository;
        public StuCompService(StuCompRepository StuCompRepository)
        {
            base.baseRepository = StuCompRepository;
            _StuCompRepository = StuCompRepository;
        }
    }
    public class QuestionService : BaseService<Question>
    {
        private readonly QuestionRepository _QuestionRepository;
        public QuestionService(QuestionRepository QuestionRepository)
        {
            base.baseRepository = QuestionRepository;
            _QuestionRepository = QuestionRepository;
        }
    }
    public class AnswerService : BaseService<Answer>
    {
        private readonly AnswerRepository _AnswerRepository;
        public AnswerService(AnswerRepository AnswerRepository)
        {
            base.baseRepository = AnswerRepository;
            _AnswerRepository = AnswerRepository;
        }
    }
    public class ProblemService : BaseService<Problem>
    {
        private readonly ProblemRepository _ProblemRepository;
        public ProblemService(ProblemRepository ProblemRepository)
        {
            base.baseRepository = ProblemRepository;
            _ProblemRepository = ProblemRepository;
        }
    }
    public class ReplyService : BaseService<Reply>
    {
        private readonly ReplyRepository _ReplyRepository;
        public ReplyService(ReplyRepository ReplyRepository)
        {
            base.baseRepository = ReplyRepository;
            _ReplyRepository = ReplyRepository;
        }
    }
    public class TeacherService : BaseService<Teacher>
    {
        private readonly TeacherRepository _TeacherRepository;
        public TeacherService(TeacherRepository TeacherRepository)
        {
            base.baseRepository = TeacherRepository;
            _TeacherRepository = TeacherRepository;
        }
    }
    public class ClassesService : BaseService<Classes>
    {
        private readonly ClassesRepository _ClassesRepository;
        public ClassesService(ClassesRepository ClassesRepository)
        {
            base.baseRepository = ClassesRepository;
            _ClassesRepository = ClassesRepository;
        }
    }

}