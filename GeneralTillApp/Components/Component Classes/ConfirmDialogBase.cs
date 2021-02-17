using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Components.Component_Classes
{
    public class ConfirmDialogBase : ComponentBase
    {
        protected bool ShowConfirmation { get; set; }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        [Parameter]
        public string DialogTitle { get; set; }
        [Parameter]
        public string DialogContents { get; set; }
        [Parameter]
        public string ConfirmButtonContents { get; set; }

        public void Show()
        {
            ShowConfirmation = true;
            StateHasChanged();
        }

        public async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;
            await ConfirmationChanged.InvokeAsync(value);
        }


    }
}
