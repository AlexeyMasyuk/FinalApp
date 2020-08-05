namespace FinalApp
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.cartList = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.Label();
            this.buyBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.sortBtn = new System.Windows.Forms.Button();
            this.catShowBtn = new System.Windows.Forms.Button();
            this.sortBox = new System.Windows.Forms.ComboBox();
            this.categoryShowBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Margin = new System.Windows.Forms.Padding(2);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 62;
            this.dgv.RowTemplate.Height = 28;
            this.dgv.Size = new System.Drawing.Size(766, 578);
            this.dgv.TabIndex = 0;
            this.dgv.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_CellDoubleClick);
            // 
            // cartList
            // 
            this.cartList.HideSelection = false;
            this.cartList.Location = new System.Drawing.Point(770, 0);
            this.cartList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 1);
            this.cartList.Name = "cartList";
            this.cartList.Size = new System.Drawing.Size(183, 183);
            this.cartList.TabIndex = 1;
            this.cartList.UseCompatibleStateImageBehavior = false;
            this.cartList.Click += new System.EventHandler(this.cartList_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(808, 194);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Total Price: ";
            // 
            // price
            // 
            this.price.AutoSize = true;
            this.price.Location = new System.Drawing.Point(885, 194);
            this.price.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(0, 13);
            this.price.TabIndex = 3;
            // 
            // buyBtn
            // 
            this.buyBtn.Location = new System.Drawing.Point(808, 222);
            this.buyBtn.Margin = new System.Windows.Forms.Padding(2);
            this.buyBtn.Name = "buyBtn";
            this.buyBtn.Size = new System.Drawing.Size(64, 25);
            this.buyBtn.TabIndex = 4;
            this.buyBtn.Text = "Buy";
            this.buyBtn.UseVisualStyleBackColor = true;
            this.buyBtn.Click += new System.EventHandler(this.buyBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(808, 251);
            this.clearBtn.Margin = new System.Windows.Forms.Padding(2);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(64, 25);
            this.clearBtn.TabIndex = 5;
            this.clearBtn.Text = "Clear Cart";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // sortBtn
            // 
            this.sortBtn.Location = new System.Drawing.Point(779, 523);
            this.sortBtn.Margin = new System.Windows.Forms.Padding(2);
            this.sortBtn.Name = "sortBtn";
            this.sortBtn.Size = new System.Drawing.Size(61, 23);
            this.sortBtn.TabIndex = 6;
            this.sortBtn.Text = "Sort By";
            this.sortBtn.UseVisualStyleBackColor = true;
            this.sortBtn.Click += new System.EventHandler(this.sortBtn_Click);
            // 
            // catShowBtn
            // 
            this.catShowBtn.Location = new System.Drawing.Point(780, 554);
            this.catShowBtn.Margin = new System.Windows.Forms.Padding(2);
            this.catShowBtn.Name = "catShowBtn";
            this.catShowBtn.Size = new System.Drawing.Size(74, 22);
            this.catShowBtn.TabIndex = 7;
            this.catShowBtn.Text = "Show Only";
            this.catShowBtn.UseVisualStyleBackColor = true;
            this.catShowBtn.Click += new System.EventHandler(this.catShowBtn_Click);
            // 
            // sortBox
            // 
            this.sortBox.FormattingEnabled = true;
            this.sortBox.Items.AddRange(new object[] {
            "Low Price",
            "High Price",
            "Name A-Z"});
            this.sortBox.Location = new System.Drawing.Point(864, 522);
            this.sortBox.Margin = new System.Windows.Forms.Padding(2);
            this.sortBox.Name = "sortBox";
            this.sortBox.Size = new System.Drawing.Size(90, 21);
            this.sortBox.TabIndex = 8;
            // 
            // categoryShowBox
            // 
            this.categoryShowBox.FormattingEnabled = true;
            this.categoryShowBox.Items.AddRange(new object[] {
            "All"});
            this.categoryShowBox.Location = new System.Drawing.Point(872, 555);
            this.categoryShowBox.Margin = new System.Windows.Forms.Padding(2);
            this.categoryShowBox.Name = "categoryShowBox";
            this.categoryShowBox.Size = new System.Drawing.Size(84, 21);
            this.categoryShowBox.TabIndex = 9;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 587);
            this.Controls.Add(this.categoryShowBox);
            this.Controls.Add(this.sortBox);
            this.Controls.Add(this.catShowBtn);
            this.Controls.Add(this.sortBtn);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.buyBtn);
            this.Controls.Add(this.price);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cartList);
            this.Controls.Add(this.dgv);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ListView cartList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label price;
        private System.Windows.Forms.Button buyBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button sortBtn;
        private System.Windows.Forms.Button catShowBtn;
        private System.Windows.Forms.ComboBox sortBox;
        private System.Windows.Forms.ComboBox categoryShowBox;
    }
}