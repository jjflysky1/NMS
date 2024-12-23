using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_run
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Main 메서드 시작");

            // 작업이 완료될 때까지 대기하기 위한 Task 배열
            Task[] tasks = new Task[5];

            bool isCompleted = false;
            while (!isCompleted)
            {
                // Parallel.For를 Task.Run으로 감싸서 사용하여 병렬로 비동기 함수 실행
                await Task.Run(() =>
                {
                    Parallel.For(0, 5, i =>
                    {
                        // Task.Run을 사용하여 각 함수를 병렬로 실행하고, 작업을 Task 배열에 저장
                        tasks[i] = Task.Run(async () => await BackgroundAsyncFunction(i));
                    });
                });

                // 모든 작업이 완료될 때까지 대기
                await Task.WhenAll(tasks);

               
             
            }
            // 작업이 모두 완료되면 while 루프 종료
            isCompleted = true;
            Console.WriteLine("모든 작업 완료");

            Console.WriteLine("Main 메서드 종료");
        }

        static async Task BackgroundAsyncFunction(int index)
        {
            // 여기에 백그라운드 비동기 함수 코드 작성
            Console.WriteLine($"백그라운드 비동기 함수 {index} 시작");
            await Task.Delay(2000); // 2초 동안 대기
            Console.WriteLine($"백그라운드 비동기 함수 {index} 완료");
        }
    }
}
