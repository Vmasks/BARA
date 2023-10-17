/***
 * 
 *    Title: "SUIFW" UI框架项目
 *           主题： 窗体类型   
 *    Description: 
 *           功能： 
 *                  
 *    Date: 2017
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SUIFW
{
	public class UIType {
        //是否清空“栈集合”
	    public bool IsClearStack = false;
        //UI窗体（位置）类型
	    public UIFormType UIForms_Type = UIFormType.Normal;
        //UI窗体显示类型
	    public UIFormShowMode UIForms_ShowMode = UIFormShowMode.Normal;
        //UI窗体透明度类型
	    public UIFormLucenyType UIForm_LucencyType = UIFormLucenyType.Lucency;

	}
}