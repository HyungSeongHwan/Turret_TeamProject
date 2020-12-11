﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVParser
{
    public static List<string[]> Load(string sPathName)
    {
        List<string[]> listData = new List<string[]>();

        TextAsset kTextAsset = (TextAsset)Resources.Load(sPathName, typeof(TextAsset));
        if (kTextAsset == null) return null;

        StringReader _reader = new StringReader(kTextAsset.text);

        string inputData = _reader.ReadLine();
        while (inputData != null)
        {
            string[] datas = inputData.Split('\t');
            if (datas.Length == 0) continue;

            listData.Add(datas);

            inputData = _reader.ReadLine();
        }

        _reader.Close();
        return listData;
    }

    // sPathName : 주의) 폴더가 Assets 부터 시작해야 한다. "Assets/Resources/TableData/test.csv"
    public static List<string[]> Load2(string sPathName)
    {
        List<string[]> listData = new List<string[]>();

        StreamReader sr = new StreamReader(sPathName, System.Text.Encoding.UTF8);
        if (sr == null) return null;

        while (!sr.EndOfStream)                     // 스트림의 끝까지 일기
        {
            string line = sr.ReadLine();            // 한 줄씩 읽어온다.

            string[] datas = line.Split('\t');      // 탭을 기준으로 문자를 분리한다.
            if (datas.Length == 0) continue;

            listData.Add(datas);
        }
        sr.Close();

        return listData;
    }
}