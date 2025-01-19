//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using Unity.Collections.LowLevel.Unsafe;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class Event_Load : Event_Data_Manager
//{
//    int[] Result_tam;
//    int Result_Num;
//    int now_array_num;
//    int Max_Probability;
//    float countTime;
//    [SerializeField] float countTime_Max;

//    [SerializeField] public EventData eventData1;
//    [SerializeField] public EventData eventData2;
//    [SerializeField] public EventData eventData3;
//    [SerializeField] public EventData eventData4;
//    [SerializeField] public EventData eventData5;
//    [SerializeField] public EventData eventData6;
//    [SerializeField] public EventData eventData7;
//    [SerializeField] public EventData eventData8;
//    [SerializeField] public EventData eventData9;
//    [SerializeField] public EventData eventData10;
//    [SerializeField] public EventData eventData11;

//    // Start is called before the first frame update
//    void Start()
//    {
//        Result_Num = 0;
//        now_array_num = 0;
//        Max_Probability = 0;

//        countTime = countTime_Max;

//        if (eventData1.Event_Flag)
//        {
//            Max_Probability += eventData1.Probability;
//        }
//        if (eventData2.Event_Flag)
//        {
//            Max_Probability += eventData2.Probability;
//        }
//        if (eventData3.Event_Flag)
//        {
//            Max_Probability += eventData3.Probability;
//        }
//        if (eventData4.Event_Flag)
//        {
//            Max_Probability += eventData4.Probability;
//        }
//        if (eventData5.Event_Flag)
//        {
//            Max_Probability += eventData5.Probability;
//        }
//        if (eventData6.Event_Flag)
//        {
//            Max_Probability += eventData6.Probability;
//        }
//        if (eventData7.Event_Flag)
//        {
//            Max_Probability += eventData7.Probability;
//        }
//        if (eventData8.Event_Flag)
//        {
//            Max_Probability += eventData8.Probability;
//        }
//        if (eventData9.Event_Flag)
//        {
//            Max_Probability += eventData9.Probability;
//        }
//        if (eventData10.Event_Flag)
//        {
//            Max_Probability += eventData10.Probability;
//        }
//        if (eventData11.Event_Flag)
//        {
//            Max_Probability += eventData11.Probability + 1;
//        }

//        Debug.Log(Max_Probability);

//        Result_tam = new int[Max_Probability];

//        if (eventData1.Event_Flag)
//        {
//            for (int i = 0; eventData1.Probability > i; i++)
//            {
//                Result_tam[i] = eventData1.Event_Num;
//            }
//            now_array_num = eventData1.Probability - 1;
//        }
//        if (eventData2.Event_Flag)
//        {
//            for (int i = 0; eventData2.Probability > i; i++)
//            {
//                Result_tam[now_array_num + i] = eventData2.Event_Num;
//            }
//            now_array_num += eventData2.Probability - 1;
//        }
//        if (eventData3.Event_Flag)
//        {
//            for (int i = 0; eventData3.Probability > i; i++)
//            {
//                Result_tam[now_array_num + i] = eventData3.Event_Num;
//            }
//            now_array_num += eventData3.Probability - 1;
//        }
//        if (eventData4.Event_Flag)
//        {
//            for (int i = 0; eventData4.Probability > i; i++)
//            {
//                Result_tam[now_array_num + i] = eventData4.Event_Num;
//            }
//            now_array_num += eventData4.Probability - 1;
//        }
//        if (eventData5.Event_Flag)
//        {
//            for (int i = 0; eventData5.Probability > i; i++)
//            {
//                Result_tam[now_array_num + i] = eventData5.Event_Num;
//            }
//            now_array_num += eventData5.Probability - 1;
//        }
//        if (eventData6.Event_Flag)
//        {
//            for (int i = 0; eventData6.Probability > i; i++)
//            {
//                Result_tam[now_array_num + i] = eventData6.Event_Num;
//            }
//            now_array_num += eventData6.Probability - 1;
//        }
//        if (eventData7.Event_Flag)
//        {
//            for (int i = 0; eventData7.Probability > i; i++)
//            {
//                Result_tam[now_array_num + i] = eventData7.Event_Num;
//            }
//            now_array_num += eventData7.Probability - 1;
//        }
//        if (eventData8.Event_Flag)
//        {
//            for (int i = 0; eventData8.Probability > i; i++)
//            {
//                Result_tam[now_array_num + i] = eventData8.Event_Num;
//            }
//            now_array_num += eventData8.Probability - 1;
//        }
//        if (eventData9.Event_Flag)
//        {
//            for (int i = 0; eventData9.Probability > i; i++)
//            {
//                Result_tam[now_array_num + i] = eventData9.Event_Num;
//            }
//            now_array_num += eventData9.Probability - 1;
//        }
//        if (eventData10.Event_Flag)
//        {
//            for (int i = 0; eventData10.Probability > i; i++)
//            {
//                Result_tam[now_array_num + i] = eventData10.Event_Num;
//            }
//            now_array_num += eventData10.Probability - 1;
//        }
//        if (eventData11.Event_Flag)
//        {
//            for (int i = 0; eventData10.Probability > i; i++)
//            {
//                Result_tam[now_array_num + i] = eventData10.Event_Num;
//            }
//            now_array_num += eventData10.Probability - 1;
//        }
//        //if (eventData10.Event_Flag)
//        //{
//        //    for (int i = 0; eventData10.Probability > i; i++)
//        //    {
//        //        Result_tam[now_array_num + i] = eventData10.Event_Num;
//        //    }
//        //    now_array_num += eventData10.Probability - 1;
//        //}
//        //if (eventData11.Event_Flag)
//        //{
//        //    for (int i = 0; eventData11.Probability > i; i++)
//        //    {
//        //        Result_tam[now_array_num + i] = eventData11.Event_Num;
//        //    }
//        //    now_array_num += eventData11.Probability - 1;
//        //}

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (countTime <= 0)
//        {
//            int Result_num_tnp = Random.Range(0, Max_Probability);
//            Result_Num = Result_tam[Result_num_tnp];
//            SceneManager.LoadScene("Event1");
//            if (Result_Num == 1)
//            {
//                setEventData(eventData1);
//            }
//            if (Result_Num == 2)
//            {
//                setEventData(eventData2);
//                //SceneManager.LoadScene("Event2");
//            }
//            if (Result_Num == 3)
//            {
//                setEventData(eventData3);
//                //SceneManager.LoadScene("Event3");
//            }
//            if (Result_Num == 4)
//            {
//                setEventData(eventData4);
//                //SceneManager.LoadScene("Event4");
//            }
//            if (Result_Num == 5)
//            {
//                setEventData(eventData5);
//                //SceneManager.LoadScene("Event5");
//            }
//            if (Result_Num == 6)
//            {
//                setEventData(eventData6);
//                //SceneManager.LoadScene("Event6");
//            }
//            if (Result_Num == 7)
//            {
//                setEventData(eventData7);
//                //SceneManager.LoadScene("Event7");
//            }
//            if (Result_Num == 8)
//            {
//                setEventData(eventData8);
//                //SceneManager.LoadScene("Event8");
//            }
//            if (Result_Num == 9)
//            {
//                setEventData(eventData9);
//                //SceneManager.LoadScene("Event9");
//            }
//            if (Result_Num == 10)
//            {
//                setEventData(eventData10);
//                //SceneManager.LoadScene("Event10");
//            }
//            //if (Result_Num == 11)
//            //{
//            //    SceneManager.LoadScene("Event11");
//            //}
//        }
//        countTime -= Time.deltaTime;
//    }
//}



