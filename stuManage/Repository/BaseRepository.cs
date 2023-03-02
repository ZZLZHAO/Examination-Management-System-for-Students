using Model;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    //基类repository，提供常用的的增删改查功能，所有的类都继承于该类
    public class BaseRepository<TEntity> : SimpleClient<TEntity> where TEntity : class, new()
    {
        //用于自动创建数据库中的表
        public BaseRepository(ISqlSugarClient context = null) : base(context)
        {
            base.Context = DbScoped.Sugar;
            base.Context.CodeFirst.InitTables(
            typeof(Announce), typeof(Customer), typeof(ExamInfo), typeof(Score),
            typeof(StuExam), typeof(Student), typeof(CompInfo), typeof(StuComp),
            typeof(Question), typeof(Answer), typeof(Reply), typeof(Problem),
            typeof(Teacher), typeof(Classes)
            );
        }
        //向表中插入一条数据
        public async Task<bool> InsertItem(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }
        //删除一条数据，该数据必须是可以唯一确定的
        public async Task<bool> DeleteItem(Expression<Func<TEntity, bool>> func)
        {
            return await base.DeleteAsync(func);
        }
        //根据主键删除一条数据

        public async Task<bool> DeleteItem(int id)
        {
            return await base.DeleteByIdAsync(id);
        }

        //根据传入的判断式返回满足条件的一条数据
        public virtual async Task<TEntity> FindItem(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }
        //获取表中所有数据
        public virtual async Task<List<TEntity>> FindItemList()
        {
            return await base.GetListAsync();
        }
        //更具判断式返回满足判断的所有数据

        public virtual async Task<List<TEntity>> FindItemList(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetListAsync(func);
        }
    }
}
