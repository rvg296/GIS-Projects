'''<summary>
'''This code when executed does the following
'''1. It will add the feature classes from the geodatabase
'''2. Runs the Near Tool on it
'''3. Queries the feature classes to find out what is near what
'''4. Provides the user with the name and the distance the feature is at
'''</summary>

Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Catalog
Imports ESRI.ArcGIS.CatalogUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Geoprocessor
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.AnalysisTools
Imports ESRI.ArcGIS.esriSystem
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.GeoprocessingUI

Public Class FindTheBustopToGo

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.ArcMap.Application.CurrentTool = Nothing
        'Define the ArcMap Document
        Dim pMxDoc As IMxDocument = My.ArcMap.Application.Document
        Dim pMap As IMap = pMxDoc.FocusMap


        'Define the geoprocessor
        Dim gp As Geoprocessor = New Geoprocessor()

        gp.SetEnvironmentValue("workspace", "C:\Users\rxm160030\Documents\ArcGIS\Projects\FinalProject\FinalProject.gdb")

        'Declare the near Tool
        Dim nearTool As Near = New Near()
        nearTool.in_features = "MajorCommunities"
        nearTool.near_features = "Stops"
        nearTool.search_radius = InputBox("Enter the radius", "Proximity", "500 Meters", -1, -1)
        nearTool.location = True
        nearTool.angle = True
        nearTool.method = InputBox("Enter the method", "Method", "PLANAR", -1, -1)
        gp.Execute(nearTool, Nothing)

        'Define the workspace where data is stored
        Dim pWorkspaceFactory As IWorkspaceFactory = New FileGDBWorkspaceFactory
        Dim pWorkSpace As IFeatureWorkspace = pWorkspaceFactory.OpenFromFile("C:\Users\rxm160030\Documents\ArcGIS\Projects\FinalProject\FinalProject.gdb", 0)
        Dim pFWS As IFeatureWorkspace = pWorkSpace

        'Define the Layer
        Dim pFL As IFeatureLayer = pMap.Layer(0)
        Dim pFC As IFeatureClass = pFL.FeatureClass


        'Add the feature class Stops
        Dim pFC1 As IFeatureClass = pFWS.OpenFeatureClass("Stops")
        Dim pFL1 As IFeatureLayer = New FeatureLayer
        pFL1.FeatureClass = pFC1
        pFL1.Name = "Stops"
        pMap.AddLayer(pFL1)

        'Add the feature class DARTRoute883West
        Dim pFC2 As IFeatureClass = pFWS.OpenFeatureClass("DARTRoute883West")
        Dim pFL2 As IFeatureLayer = New FeatureLayer
        pFL2.FeatureClass = pFC2
        pFL2.Name = "DARTRoute883West"
        pMap.AddLayer(pFL2)

        'Add the feature class MccallumExpressRoute
        Dim pFC3 As IFeatureClass = pFWS.OpenFeatureClass("MccallumExpressRoute")
        Dim pFL3 As IFeatureLayer = New FeatureLayer
        pFL3.FeatureClass = pFC3
        pFL3.Name = "MccallumExpressRoute"
        pMap.AddLayer(pFL3)

        'Add the feature class DARTRoute883WestExtension
        Dim pFC4 As IFeatureClass = pFWS.OpenFeatureClass("DARTRoute883WestExtension")
        Dim pFL4 As IFeatureLayer = New FeatureLayer
        pFL4.FeatureClass = pFC4
        pFL4.Name = "DARTRoute883WestExtension"
        pMap.AddLayer(pFL4)

        'Add the feature class DARTRoute883East
        Dim pFC5 As IFeatureClass = pFWS.OpenFeatureClass("DARTRoute883East")
        Dim pFL5 As IFeatureLayer = New FeatureLayer
        pFL5.FeatureClass = pFC5
        pFL5.Name = "DARTRoute883East"
        pMap.AddLayer(pFL5)

        'Add the feature class CityLineBushExpressPilotRoute
        Dim pFC6 As IFeatureClass = pFWS.OpenFeatureClass("CityLineBushExpressPilotRoute")
        Dim pFL6 As IFeatureLayer = New FeatureLayer
        pFL6.FeatureClass = pFC6
        pFL6.Name = "CityLineBushExpressPilotRoute"
        pMap.AddLayer(pFL6)

        'Add the feature Class MajorStops
        Dim pFC7 As IFeatureClass = pFWS.OpenFeatureClass("MajorStops")
        Dim pFL7 As IFeatureLayer = New FeatureLayer
        pFL7.FeatureClass = pFC7
        pFL7.Name = "MajorStops"
        pMap.AddLayer(pFL7)


        'Check if the student lives on the west side of university
        If ComboBox3.Text = "West" Then
            'Now check for the apartments he live in
            If ComboBox1.Text = "ChathamCourts" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'ChathamCourts'"
                'Query with the apartment name
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                'Find the distance field so as to give the distance from apartment to bustop
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                'Get the feature ID
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                'Query the feature ID which is same as object ID
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                'Give the name of the bustop
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)

                'Display the nearest bustop and its distance
                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")

                'Select the apartment to make it displayed.
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                'Select the nearest bustop to make it displayed
                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

            End If

            If ComboBox1.Text = "ChathamReflections" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'ChathamReflections'"
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)


                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            End If

            If ComboBox1.Text = "GablesOnMcCallum" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'GablesOnMcCallum'"
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)


                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            End If

            If ComboBox1.Text = "McCallumCorners" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'McCallumCorners'"
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)


                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            End If

            If ComboBox1.Text = "McCallumCrossings" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'McCallumCrossings'"
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)


                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            End If
            If ComboBox1.Text = "McCallumGlen" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'McCallumGlen'"
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)


                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            End If
            If ComboBox1.Text = "McCallumMeadows" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'McCallumMeadows'"
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)


                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            End If

            If ComboBox1.Text = "AshwoodApts" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'AshwoodApts'"
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)


                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            End If
        End If

        If ComboBox3.Text = "East" Then
            If ComboBox2.Text = "AltaCreeksideApartments" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'AltaCreeksideApartments'"
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)


                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            End If

            If ComboBox2.Text = "PrairieCreekVillas" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'PrairieCreekVillas'"
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)


                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            End If

            If ComboBox2.Text = "PraderaApartments" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'PraderaApartments'"
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)


                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            End If

            If ComboBox2.Text = "MarquisWaterviewApartments" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'MarquisWaterviewApartments'"
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)


                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            End If

            If ComboBox2.Text = "HomesOfPrairieSprings" Then
                Dim pQFilter As IQueryFilter = New QueryFilter
                pQFilter.WhereClause = "Name = 'HomesOfPrairieSprings'"
                Dim pFeatCur As IFeatureCursor = pFC.Search(pQFilter, False)
                Dim pFields As IFields = pFC.Fields
                Dim PFeature As IFeature = pFeatCur.NextFeature
                Dim pFldIdx As Integer
                Dim pFldIdx1 As Integer
                pFldIdx = pFields.FindField("NEAR_DIST")
                Dim pNearDist As Double
                pNearDist = PFeature.Value(pFldIdx)
                pFldIdx1 = pFields.FindField("NEAR_FID")
                Dim pFID As Double
                pFID = PFeature.Value(pFldIdx1)

                Dim pQFilter1 As IQueryFilter = New QueryFilter
                pQFilter1.WhereClause = "OBJECTID = " & CStr(pFID)
                Dim pFeatCur1 As IFeatureCursor = pFC1.Search(pQFilter1, False)
                Dim pFields1 As IFields = pFC1.Fields
                Dim PFeature1 As IFeature = pFeatCur1.NextFeature
                Dim pFldIdx3 As Integer
                pFldIdx3 = pFields1.FindField("StationName")
                Dim pSName As String
                pSName = PFeature1.Value(pFldIdx3)


                MsgBox("The nearest bustop to your apartment is " & pSName & vbCrLf & "This is at a distance of " & Math.Round(pNearDist) & " Meters")
                Dim pFSel As IFeatureSelection
                pFSel = pFL
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel.SelectFeatures(pQFilter, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)

                Dim pFSel1 As IFeatureSelection
                pFSel1 = pFL1
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
                pFSel1.SelectFeatures(pQFilter1, esriSelectionResultEnum.esriSelectionResultAdd, False)
                pMxDoc.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, Nothing)
            End If
        End If

    End Sub

    'Select the side where you live
    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text = "West" Then
            ComboBox2.Enabled = False
        Else
            ComboBox1.Enabled = False
        End If

    End Sub

    'Close the form
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()
    End Sub


    'Generate the Theissen Polygons to find the area that is served by Bustops
    Private Sub Theissen_Click(sender As Object, e As EventArgs) Handles Theissen.Click
        'Define the ArcMap Document
        Dim pMxDoc As IMxDocument = My.ArcMap.Application.Document
        Dim pMap As IMap = pMxDoc.FocusMap

        'Define the geoprocessor
        Dim gp As Geoprocessor = New Geoprocessor()

        gp.SetEnvironmentValue("workspace", "C:\Users\rxm160030\Documents\ArcGIS\Projects\FinalProject\FinalProject.gdb")

        Dim CTP As CreateThiessenPolygons = New CreateThiessenPolygons
        CTP.in_features = "Stops"
        CTP.out_feature_class = "Stops_Theissen"
        CTP.fields_to_copy = "ALL"

        gp.Execute(CTP, Nothing)
    End Sub

    'Load the BaseMap from ArcGIS Online
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim pMxDoc As IMxDocument = My.ArcMap.Application.Document
        Dim pMap As IMap = pMxDoc.FocusMap

        Dim layerFile As ILayerFile = New LayerFileClass
        layerFile.Open("http://www.arcgis.com/sharing/content/items/b6969de2b84d441692f5bb8792e65d1f/item.pkinfo")
        Dim layer As ILayer = layerFile.Layer
        pMap.AddLayer(layer)
    End Sub

    'Zoom to the contents of the ArcMap
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim pMxDoc As IMxDocument
        Dim pMap As IMap
        Dim pActiveView As IActiveView
        Dim pContentsView As IContentsView
        Dim pLayer As ILayer

        pMxDoc = My.ArcMap.Application.Document
        pMap = pMxDoc.FocusMap
        pActiveView = pMap
        pContentsView = pMxDoc.CurrentContentsView

        If Not TypeOf pContentsView.SelectedItem Is ILayer Then Exit Sub
        pLayer = pContentsView.SelectedItem
        pActiveView.Extent = pLayer.AreaOfInterest
        pActiveView.Refresh()
    End Sub
End Class