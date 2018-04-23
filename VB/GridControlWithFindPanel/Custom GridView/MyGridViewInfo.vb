Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid

Namespace GridControlWithFindPanel
	Public Class MyGridViewInfo
		Inherits GridViewInfo
		Public Sub New(ByVal gridView As GridView)
			MyBase.New(gridView)
		End Sub

		Public Overrides Sub CalcRects(ByVal bounds As Rectangle, ByVal partital As Boolean)
			Dim inTop As Boolean = (TryCast(Me.View, MyGridView)).ShowFindPanelOnTop
			Dim r As Rectangle = Rectangle.Empty
			Dim myBounds As Rectangle = bounds
			If (Not inTop) Then
				myBounds.Height = bounds.Height - 45
			End If
			ViewRects.Bounds = myBounds
			ViewRects.Scroll = CalcScrollRect()
			ViewRects.Client = CalcClientRect()
			FilterPanel.Bounds = Rectangle.Empty
			If (Not partital) Then
				CalcRectsConstants()
			End If
			If View.OptionsView.ShowIndicator Then
				ViewRects.IndicatorWidth = Math.Max(View.IndicatorWidth, ViewRects.MinIndicatorWidth)
			End If
			Dim minTop As Integer = ViewRects.Client.Top
			Dim maxBottom As Integer = ViewRects.Client.Bottom
			If View.OptionsView.ShowViewCaption Then
				r = ViewRects.Client
				r.Y = minTop
				r.Height = CalcViewCaptionHeight(ViewRects.Client)
				ViewRects.ViewCaption = r
				minTop = ViewRects.ViewCaption.Bottom
			End If
			If inTop Then
				minTop = UpdateFindControlVisibility(New Rectangle(ViewRects.Client.X, minTop, ViewRects.Client.Width, maxBottom - minTop)).Y
			Else
				UpdateFindControlVisibility(New Rectangle(ViewRects.Client.X, ViewRects.Client.Bottom, ViewRects.Client.Width, maxBottom - minTop))
			End If
			If View.OptionsView.ShowGroupPanel Then
				r = ViewRects.Client
				r.Y = minTop
				r.Height = CalcGroupPanelHeight()
				ViewRects.GroupPanel = r
				minTop = ViewRects.GroupPanel.Bottom
			End If
			minTop = CalcRectsColumnPanel(minTop)
			If View.IsShowFilterPanel Then
				r = ViewRects.Client
				Dim fPanel As Integer = GetFilterPanelHeight()
				r.Y = maxBottom - fPanel
				r.Height = fPanel
				FilterPanel.Bounds = r
				maxBottom = r.Top
			End If
			If View.OptionsView.ShowFooter Then
				r = ViewRects.Client
				r.Height = GetFooterPanelHeight()
				r.Y = maxBottom - r.Height
				ViewRects.Footer = r
				maxBottom = r.Top
			End If
			r = ViewRects.Client
			r.Y = minTop
			r.Height = maxBottom - minTop
			ViewRects.Rows = r
		End Sub
	End Class
End Namespace
