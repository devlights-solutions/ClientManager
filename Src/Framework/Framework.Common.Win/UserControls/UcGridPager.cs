using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace Framework.Common.Win.UserControls
{
    public partial class UcGridPager : UserControl
    {
        private const int DefaultPageSize = 20;

        private bool _isCurrentPageTextChangedEnable; 
        private bool _isPagerLoaded;

        public UcGridPager()
        {
            InitializeComponent();

            InitializePager();
        }

        public Func<int> RefreshAction { get; set; }

        public Func<Task<int>> RefreshActionAsync { get; set; }

        public int CurrentPage
        {
            get
            {
                if (string.IsNullOrEmpty(TxtCurrentPage.Text))
                    return 1;

                return Convert.ToInt32(TxtCurrentPage.Text);
            }
            set
            {
                if (value == 0)
                {
                    value = 1;
                }

                _isCurrentPageTextChangedEnable = false;

                TxtCurrentPage.Text = value.ToString();

                _isCurrentPageTextChangedEnable = true;
            }
        }

        public int PageSize
        {
            get
            {
                return Convert.ToInt32(CbxPageSize.Text);
            }
            set
            {
                CbxPageSize.SelectedValue = value.ToString();
            }
        }

        public int PageTotal
        {
            get
            {
                if (string.IsNullOrEmpty(TxtPageTotal.Text))
                    return 0;

                return Convert.ToInt32(TxtPageTotal.Text);
            }
            set
            {
                TxtPageTotal.Text = value.ToString();
            }
        }

        public void UpdateState(int pageTotal)
        {
            var diff = Decimal.Parse(pageTotal.ToString()) / Decimal.Parse(PageSize.ToString());

            PageTotal = Convert.ToInt32(Math.Ceiling(diff));

            BtnPreviousPage.Enabled = CurrentPage != 1;
            BtnFirstPage.Enabled = CurrentPage != 1;
            BtnNextPage.Enabled = CurrentPage < PageTotal;
            BtnLastPage.Enabled = CurrentPage < PageTotal;
        }

        public bool CanGoNextPage
        {
            get { return PageTotal > CurrentPage; }
        }

        public bool CanGoPreviousPage
        {
            get { return CurrentPage > 1; }
        }

        private void InitializePager()
        {
            PageSize = DefaultPageSize;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            this.BtnNextPage.Click += OnNextButtonClick;
            this.BtnPreviousPage.Click += OnPreviousButtonClick;
            this.BtnFirstPage.Click += OnFirstButtonClick;
            this.BtnLastPage.Click += OnLastButtonClick;
            this.TxtCurrentPage.TextChanged += TxtCurrentPageOnTextChanged;
            this.CbxPageSize.SelectedIndexChanged += CbxPageSizeOnSelectedValueChanged;
        }

        private async void CbxPageSizeOnSelectedValueChanged(object sender, EventArgs eventArgs)
        {
            if (!_isPagerLoaded)
            {
                return;
            }

            CurrentPage = 1;
            var total = await InvokeRefreshAction();
            UpdateState(total);
        }

        private async void OnFirstButtonClick(object sender, EventArgs e)
        {
            if (this.CanGoPreviousPage)
            {
                this.CurrentPage = 1;
                var total = await InvokeRefreshAction();
                UpdateState(total);
            }
        }

        private async void OnLastButtonClick(object sender, EventArgs e)
        {
            if (this.CanGoNextPage)
            {
                this.CurrentPage = PageTotal;
                var total = await InvokeRefreshAction();
                UpdateState(total);
            }
        }

        private async void OnPreviousButtonClick(object sender, EventArgs e)
        {
            if (this.CanGoPreviousPage)
            {

                this.CurrentPage = this.CurrentPage - 1;
                var total = await InvokeRefreshAction();
                UpdateState(total);
            }
        }

        private async void OnNextButtonClick(object sender, EventArgs args)
        {
            if (this.CanGoNextPage)
            {
                this.CurrentPage = this.CurrentPage + 1;
                var total = await InvokeRefreshAction();
                UpdateState(total);
            }
        }

        private async void TxtCurrentPageOnTextChanged(object sender, EventArgs eventArgs)
        {
            if (!_isCurrentPageTextChangedEnable)
                return;

            if (CurrentPage > PageTotal)
            {
                CurrentPage = PageTotal;
            }

            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }
            var total = await InvokeRefreshAction();
            UpdateState(total);
        }

        private Task<int> InvokeRefreshAction()
        {
            if (RefreshAction != null)
            {
                return new Task<int>(() => RefreshAction());
            }
            else
            {
                return RefreshActionAsync();
            }
        }

        private void UcGridPager_Load(object sender, EventArgs e)
        {
            _isPagerLoaded = true;
        }
    }
}
