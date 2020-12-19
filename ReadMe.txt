Title:	"PuppyBreath"
Description:	"A simplistic and robust 2D video game engine for .Net Windows Forms,
				based entirely on managed code and GDI+ graphics."
Authored in:	"VisualBasic.Net"

This code and related source files are licensed and copyright as follows:

Copyright:	"Name, logo, design and original development Copyright © 2018-2020 Reed Kimble"
Code File License:	"Code files are licensed pursuant to the [MIT License] (see License.txt)"

NuGet Link:	https://www.nuget.org/packages/ReedKimble.PuppyBreath/

Last Updated: 12/19/2020

Abstract:
	This library is the result of many attempts at creating an easy to use and performant game engine
	for use in a Windows Forms project and benefits from my having spent a lot of time with Unity3D. As
	a result it provides an easy to use and versatile framework for creating a video game, while still
	providing respectable performance for the majority of the requirements of a "retro" video game.

	This framework is intended for hobbist programmers and/or students who want to create small games
	for their own personal use and/or limited distribution among friends.  That's not to say that the
	framework could not support a game of mass distribution, in theory it could, however it is not
	aimed at that and therefore doesn't claim to be suitable (for instance, there is no cross-platform
	support provided).

	PuppyBreath is a 2D rendering engine based on GDI+ graphics. While not supplied with the library,
	you could create classes to generate simulated 3D effects using 2D drawing techniques.

Getting Started:
	To get started, add a RenderCanvas compontent to your Form1 and then edit the default code as follows.
	This replaces any previous guidance prior to version 0.4.0.0.

	(*Note the addition of the Async keyword on Sub Form1_Load)

	[EXAMPLE VB.NET]
		Imports PuppyBreath

		Public Class Form1
			Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
				RenderCanvas1.SceneManager.Add(New MainScene)
				RenderCanvas1.SceneManager.ChangeScene("Main Scene")
				Await RenderCanvas1.BeginAsync
			End Sub

			Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
				RenderCanvas1.StopRenderer()
			End Sub
		End Class

		Public Class MainScene
			Inherits GameScene

			Public Sub New()
				Me.Name = "Main Scene"
			End Sub

			Protected Overrides Sub Initialize(state As GameState)
				MyBase.Initialize(state)
				'add initial game object instances to Me.GameObjects collection
				'and configure the game world
			End Sub
		End Class
	[\EXAMPLE]

Known Limitations:
	+ Colliders do not scale with sprite size.  You must size your colliders to fit the intended scaling of your sprites.
	  Changing a sprite's scale during runtime requires that you also change the collider size to match.
	  (Yes, adding Transforms would fix this... I'm considering it.)
	+ There is no built-in way of handling a logical game object consisting of multiple sprites; you will need to create
	  your own means of synchronizing mutiple Sprite objects to act as a single entity

FAQ:

Q: What if the RenderCanvas does not appear in the Toolbox?
A: Right click the Toolbox and select "Choose items..."
   When the dialog loads, click the browse button.
   Navigate to the current solution directory and then to packages\ReedKimble.PuppyBreath.0.2.0\lib\net461
   Select the PuppyBreath.dll file and click OK.  The RenderCanvas will be added in the dialog. Click OK
   to return to Visual Studio and the icon will then appear in the Toolbox.



[Obsolete]
The original discussion may still provide direction and ideas, but you may have to adapt the sample code to work with
the current version of the framework and some functionality may now be handled differently than originally described.

Walkthrough/Tutorial & Discussion:
	https://social.msdn.microsoft.com/Forums/en-US/2440752f-66e5-4995-93c4-e018ce43efc9/how-to-get-started-with-video-game-development-in-visual-basic-net-using-the-puppybreath?forum=vbgeneral
