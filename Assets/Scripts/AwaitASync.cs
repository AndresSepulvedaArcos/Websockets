 
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AwaitASync : MonoBehaviour
{
    System.Action<Task<int>> action;
   
    Task<int> promiseInt;
    CancellationTokenSource cancellationToken;
    // Start is called before the first frame update
    void Start()
    { 
        //StartCoroutine(WaitTimer());
        //WaitTimerTask();
          cancellationToken=new CancellationTokenSource();
        promiseInt = WaitForReturn(cancellationToken);
       
        promiseInt.ContinueWith((result)=>{
            Debug.Log(result.Result);
        });
         
      Debug.Log("esto sigue");  
    }

    private void OnDisable()
    {
        
    }


    void ExitGame()
    {
        Debug.Log("Exiting");
        try
        {
            cancellationToken.Cancel();
         //   promiseInt.
        }catch(TaskCanceledException)
        {
           

        }



         Debug.Log(cancellationToken.IsCancellationRequested);
        Application.Quit();

    }

   
    IEnumerator WaitForReturnInt()
    {
        int i = 0;
        int maxRange = Random.Range(1000, 1500);
        while (i < maxRange)
        {
            i++;
        }
       

        yield return new WaitForEndOfFrame();
       yield return i;

    }
    async Task<int> WaitForReturn( CancellationTokenSource cancellationToken  )
    { 
        int i=0;
        int maxRange= Random.Range(1000,1500);
        while(i< maxRange)
        {
            i++;
        }
        Debug.Log(cancellationToken.IsCancellationRequested);

      
        await Task.Delay(5000);
        Debug.Log(cancellationToken.IsCancellationRequested);

       
        await Task.Yield();
        return i;

    }


    async void WaitTimerTask()
    {
        Debug.Log("Before " + Time.realtimeSinceStartup.ToString());
        await Task.Delay(4000);

        Debug.Log("After " + Time.realtimeSinceStartup.ToString());
    }


     IEnumerator WaitTimer()
    {
        Debug.Log("Before "+Time.realtimeSinceStartup.ToString());
        yield return new WaitForSeconds(4);
        Debug.Log("After " + Time.realtimeSinceStartup.ToString());

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            ExitGame();
        }
    }
}
