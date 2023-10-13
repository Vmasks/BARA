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
	public string positive;
	public string negative;
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}