using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RemoteControl
{
    protected abstract void On();
    protected abstract void Off();

    protected void SetChannel(int i)
    {

    }

}
#region 橋梁
public abstract class BrushPenAbstraction
{
    /**
     * 保留对颜色的引用
     */
    protected ImplementorColor imp;

    /**
     * 每种笔都有自己的实现
     */
    public abstract void operationDraw();

    public void setImplementor(ImplementorColor imp)
    {
        this.imp = imp;
    }
}
/**
 * 粗毛笔的实现
 * @author 98583
 *
 */
public class BigBrushPenRefinedAbstraction : BrushPenAbstraction
{

    public override void operationDraw()
    {
        Debug.Log("Big and " + imp.bepaint() + " drawing!");
    }
}
public class OncreteImplementorRed : ImplementorColor
{


    public override string bepaint()
    {
        return "red";
    }
}
public class Client
{

    public static void main(string[] args)
    {
        BrushPenAbstraction brushPen = new BigBrushPenRefinedAbstraction();
        ImplementorColor col = new OncreteImplementorRed();
        /**
         * 设置颜色
         */
        brushPen.setImplementor(col);
        /**
         * 画画
         */
        brushPen.operationDraw();
    }

}
/**
 * 颜色的接口
 * @author 98583
 *
 */
public abstract class ImplementorColor
{

    public abstract string bepaint();
}
#endregion

#region 建立者
//具體的產品.
public class Product
{
    IList<string> parts = new List<string>();

    public void Add(string part)
    {
        parts.Add(part);
    }
    public void Show()
    {
        Console.WriteLine("\n產品 建立 ----");
        foreach (string part in parts)
        {
            Console.WriteLine(part);
        }
    }
}
// 建造者.
public abstract class Builder
{
    public abstract void BuildPartA();
    public abstract void BuildPartB();
    public abstract Product GetResult();

}

public class ConcreteBuilder1 : Builder
{
    private Product product = new Product();

    public override void BuildPartA()
    {
        product.Add("部件A");
    }
    public override void BuildPartB()
    {
        product.Add("部件B");
    }

    public override Product GetResult()
    {
        return product;
    }
}

public class ConcreteBuilder2 : Builder
{
    private Product product = new Product();
    public override void BuildPartA()
    {
        product.Add("部件X");
    }
    public override void BuildPartB()
    {
        product.Add("部件Y");
    }
    public override Product GetResult()
    {
        return product;
    }
}
//導演
public class Director
{
    public void Construct(Builder builder)
    {
        builder.BuildPartA();
        builder.BuildPartB();
    }
}
public class Program
{
    static void Main(string[] args)
    {
        //導演
        Director director = new Director();
        //建造者
        Builder b1 = new ConcreteBuilder1();
        Builder b2 = new ConcreteBuilder2();

        director.Construct(b1);

        Product p1 = b1.GetResult();
        p1.Show();
        director.Construct(b2);
        Product p2 = b2.GetResult();
        p2.Show();

        Console.Read();
    }
}
#endregion

public class 責任鏈模式 {

    static void Main(string[] args)
    {
        // 管理者初始化
        Manager a1 = new Manager("阿福"); // 經理
        Director a2 = new Director("技安"); // 協理
        GeneralManager a3 = new GeneralManager("宜靜"); // 總經理
        a1.SetUpManager(a2); // 設定經理的上級為協理
        a2.SetUpManager(a3); // 設定協理的上級為總經理

        // 假單初始化
        LeaveRequest leaveRequest = new LeaveRequest(); // 假單
        leaveRequest.Name = "大雄"; // 員工姓名

        leaveRequest.DayNum = 1; // 請假天數
        a1.RequestPersonalLeave(leaveRequest);// 送出1天的假單

        leaveRequest.DayNum = 3; // 請假天數
        a1.RequestPersonalLeave(leaveRequest);// 送出3天的假單

        leaveRequest.DayNum = 7; // 請假天數
        a1.RequestPersonalLeave(leaveRequest);// 送出7天的假單

        leaveRequest.DayNum = 10; // 請假天數
        a1.RequestPersonalLeave(leaveRequest);// 送出10天的假單

        Console.Read();
    }

    // 管理者處理事假申請的抽象類別
    abstract class ManagerHandler
    {
        protected string name;
        protected ManagerHandler upManager; // 上一級的管理者

        public ManagerHandler(string name)
        {
            this.name = name;
        }

        // 設定上一級的管理者
        public void SetUpManager(ManagerHandler upManager)
        {
            this.upManager = upManager;
        }

        // 事假處理
        abstract public void RequestPersonalLeave(LeaveRequest leaveRequest);
    }

    // 經理
    class Manager : ManagerHandler
    {
        public Manager(string name) : base(name) { }

        public override void RequestPersonalLeave(LeaveRequest leaveRequest)
        {
            if (leaveRequest.DayNum <= 2)
            {
                // 2天以內，經理可以批准
                Console.WriteLine("經理 {0} 已批准 {1}{2}天的事假", this.name, leaveRequest.Name, leaveRequest.DayNum);
            }
            else
            {
                // 超過2天，轉呈上級
                if (null != upManager)
                {
                    upManager.RequestPersonalLeave(leaveRequest);
                }
            }
        }
    }

    // 協理
    class Director : ManagerHandler
    {
        public Director(string name) : base(name) { }

        public override void RequestPersonalLeave(LeaveRequest leaveRequest)
        {
            if (leaveRequest.DayNum <= 5)
            {
                // 5天以內，經理可以批准
                Console.WriteLine("協理 {0} 已批准 {1}{2}天的事假", this.name, leaveRequest.Name, leaveRequest.DayNum);
            }
            else
            {
                // 超過5天，轉呈上級
                if (null != upManager)
                {
                    upManager.RequestPersonalLeave(leaveRequest);
                }
            }
        }
    }

    // 總經理
    class GeneralManager : ManagerHandler
    {
        public GeneralManager(string name) : base(name) { }

        public override void RequestPersonalLeave(LeaveRequest leaveRequest)
        {
            if (leaveRequest.DayNum <= 7)
            {
                // 7天以內，總經理批准
                Console.WriteLine("總經理 {0} 已批准 {1}{2}天的事假", this.name, leaveRequest.Name, leaveRequest.DayNum);
            }
            else
            {
                // 超過7天以上，再深入了解原因
                Console.WriteLine("總經理 {0} 要再了解 {1}{2}天的事假原因", this.name, leaveRequest.Name, leaveRequest.DayNum);
            }
        }
    }


    // 假單
    class LeaveRequest
    {
        // 姓名
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // 天數
        private int dayNum;
        public int DayNum
        {
            get { return dayNum; }
            set { dayNum = value; }
        }
    }
}