using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class Branch : Node {
	[Input]
	public Empty input;
	[Output]
	public Empty posOutput;
	[Output]
	public Empty negOutput;
	// Use this for initialization
	public string posDesc;
	public string negDesc;
	protected override void Init() {
		base.Init();
		
	}
	public Node MoveNext(bool selection)
	{
		if (selection)
		{
			NodePort posPort = GetOutputPort("posOutput");
			if (!posPort.IsConnected)
			{
				return null;
			}
			return posPort.Connection.node;
		}
		else
		{
			NodePort negPort = GetOutputPort("negOutput");
			if (!negPort.IsConnected)
			{
				return null;
			}
			return negPort.Connection.node;
		}
	}
	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}