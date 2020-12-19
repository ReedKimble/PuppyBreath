
Public Interface IRenderable
    Property Visible As Boolean
    Property ZOrder As Single
    Sub Render(g As Graphics, state As GameState)
End Interface