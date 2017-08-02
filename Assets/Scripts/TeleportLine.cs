using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLine : MonoBehaviour {

    /*for testing
    public Transform p00;
    public Transform p01;
    public Transform p02;
    public LineRenderer DrawLine;

    public int CalNum = 100;

    public bool test = false;

    private void Update()
    {
        if (test||p00.hasChanged||p01.hasChanged||p02.hasChanged)
        {
            Vector3 P00Pos = p00.position;
            Vector3 P01Pos = p01.position;
            Vector3 P02Pos = p02.position;
            TeleportLine.DrawBezierLine(DrawLine, P00Pos, P01Pos, P02Pos, CalNum);
            test = false;
            p00.hasChanged = false;
            p01.hasChanged = false;
            p02.hasChanged = false;
        }
    }
    */


    public static void DrawBezierLine(LineRenderer Line, Vector3 DrawStartPoint, Vector3 DrawInterPoint, Vector3 DrawEndPoint, int DrawNum = 100)
    {
        for(int i = 0; i < (DrawNum+1); i++)
        {
            float CalVal = (float)i / DrawNum;
            Vector3 TempBezierPoint = BezierPoint(CalVal, DrawStartPoint, DrawInterPoint, DrawEndPoint);
            Line.numPositions = DrawNum+1;
            Line.SetPosition(i,TempBezierPoint);
        }
    }


    private static Vector3 BezierPoint(float CalVal,Vector3 StartPoint,Vector3 InterPoint,Vector3 EndPoint)
    {
        if (CalVal < 0 || CalVal > 1)
        {
            Debug.Log("The Value is beyond Calculate!!");
        }
        float P0 = Mathf.Pow(1-CalVal,2);
        float P1 = 2 * CalVal * (1 - CalVal);
        float P2 = Mathf.Pow(CalVal,2);
        return (P0 * StartPoint + P1 * InterPoint + P2 * EndPoint);
    }
	
}
