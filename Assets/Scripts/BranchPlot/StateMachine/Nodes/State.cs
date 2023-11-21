using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class State : Node
{
	[Input]
	public Empty input;
	[Output]
	public Empty output;
	public DialogGraph dg;

	// Use this for initialization
	protected override void Init()
	{
		base.Init();


	}
	public Node MoveNext()
	{
		NodePort port = GetOutputPort("output");
		if (!port.IsConnected)
		{
			return null;
		}
		return port.Connection.node;
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port)
	{
		return null; // Replace this
	}
}