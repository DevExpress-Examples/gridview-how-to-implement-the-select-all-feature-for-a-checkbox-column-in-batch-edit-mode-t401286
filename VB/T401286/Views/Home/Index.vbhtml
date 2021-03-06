﻿<script type="text/javascript">
    var visibleIndex;
    var DeletedValue;
    function OnInitHeader(s, e) {
        setTimeout(function () { CheckSelectedCellsOnPage("usualCheck"); }, 0);
    }
    function OnHeaderCheckBoxCheckedChanged(s, e) {
        var visibleIndices = Grid.batchEditApi.GetRowVisibleIndices();
        var totalRowsCountOnPage = visibleIndices.length;
        for (var i = 0; i < totalRowsCountOnPage ; i++) {
            Grid.batchEditApi.SetCellValue(visibleIndices[i], "IsReserved", s.GetChecked())
        }
    }
    function OnCellCheckedChanged(s, e) {
        Grid.batchEditApi.SetCellValue(visibleIndex, "IsReserved", s.GetValue());
        CheckSelectedCellsOnPage("usualCheck");
    }
    function OnBatchEditRowDeleting(s, e) {
        DeletedValue = Grid.batchEditApi.GetCellValue(e.visibleIndex, "IsReserved");
        CheckSelectedCellsOnPage("deleteCheck");
    }
    function OnBatchEditRowInserting(s, e) {
        CheckSelectedCellsOnPage("insertCheck");
    }

    function OnBatchEditStartEditing(s, e) {
        visibleIndex = e.visibleIndex;
    }

    function CheckSelectedCellsOnPage(checkType) {
        var currentlySelectedRowsCount = 0;
        var visibleIndices = Grid.batchEditApi.GetRowVisibleIndices();
        var totalRowsCountOnPage = visibleIndices.length;
        for (var i = 0; i < totalRowsCountOnPage ; i++) {
            if (Grid.batchEditApi.GetCellValue(visibleIndices[i], "IsReserved"))
                currentlySelectedRowsCount++;
        }
        if (checkType == "insertCheck")
            totalRowsCountOnPage++;
        else if (checkType == "deleteCheck") {
            totalRowsCountOnPage--;
            if (DeletedValue)
                currentlySelectedRowsCount--;
        }
        if (currentlySelectedRowsCount <= 0)
            HeaderCheckBox.SetCheckState("Unchecked");
        else if (currentlySelectedRowsCount >= totalRowsCountOnPage)
            HeaderCheckBox.SetCheckState("Checked");
        else
            HeaderCheckBox.SetCheckState("Indeterminate");
    }
</script>
@Using (Html.BeginForm())
    @Html.Action("GridViewPartialView")
End Using
