   M S B A M L   `   `   `  ����B  >Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Manager.UserControlsManager.UserControls  5      K*clr-namespace:Manager.Utilities.ConvertersManager.Utilities.Converters  5      /    )Manager.UserControls.OrderFormUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Manager.UserControls     5      A
converters*clr-namespace:Manager.Utilities.Converters     5        � 	Resources  5      7   1Manager.Utilities.Converters.TotalAmountConverter   TotalAmountConverter8   2Manager.Utilities.Converters.ObjectStatusConverter  VisibilityConverter>   8Manager.Utilities.Converters.InvertObjectStatusConverter  InvertObjectStatusConverter4   .Manager.Utilities.Converters.DateTimeConverter  DateTimeConverter��%t   &	        &	      &	 &     &	 9       5   
   6*     5   
   6+     5   
   61     5   
   6'   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �  � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� $
��Left=�6   $��
Order Date��5       �� 5!      $��1��6   ��5"      �� *OrderForm.OrderDate+ �� 	Converter# [  �� ConverterParameter$
 Date��y�5#      �� *	OrderForm+# [ $ ViewChallan��(  "System.Windows.Controls.DatePicker  5$      $��1��6      DisplayDate 5%      �� *OrderForm.OrderDate+ �� Mode$ TwoWay=�y�5&      �� *	OrderForm+# [ $ EditOrderForm��5'      � 5*   
   $��1��6   y�6   �� *	OrderForm+# [ $ EditOrderForm�� 5+      �� 5,      �� 5-       5/      �� 50      $��4* �6#   �� 51      $��* �6#   �� 52      $��* �6#   �� 53      $��* �6#   .+�56      �� 	Item Name6#   �� 57      $��1��6   Qty6-   �� 58      $��2��6   Rate6.   �� 59      $��3��6   Total6/   �� 5;      -   5         ��Name$ combo_ItemsList��5=      $��1��5<      ��5;      �� *	ItemsList+ �� SelectedItem 5>      �� *NewOrderItem.Item+$ TwoWay=���5A      �� 5B      .R�5C      �� ��6$   �� *
ItemName+5D      5F      �� 5H      -   5        	 ��Name$	 text_Qty��5J      $��1��5H      $��1��5I      ��5K      �� *NewOrderItem.Qty+$ TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=�5L      �� 5N      -   5        $	 	text_Rate��5P      $��2��5N      $��1��5O      ��5Q      �� *NewOrderItem.AdjustedRate+$ TwoWay=�$
 PropertyChanged=�5R      � 5T      $��3��6   $��1��5U       5V      �� 5W      $��2* �6'   �� 5X      $��1* �6'   .+�5Z      ��  �� 
IsReadOnly . .��6,   ��5[      `� 5\       `� 	Converter# [  6'   .1�5]      ��  �� Path$ NewOrderItem�6&   �� 5^      $ NewOrderItem.Qty�6&   �� 5_      $ NewOrderItem.AdjustedRate�6&   �� 5`      $ NewOrderItem.Item�6&   5a      .��5b   $   5c      �� 5d      -   5         ��Name$ but_AddOrderItem��5f      $	��Add��5d      $��1��5e      5g      5h      5i      &   System.Windows.Controls.DataGrid  5l   
   $��2��6   $��Stretch=�5o         AutoGenerateColumns .     AlternatingRowBackground
 �����   CanUserAddRows .     CanUserResizeRows .     CanUserResizeColumns .     CanUserSortColumns .   � DataContext 5m      �� *	OrderForm+��5n      �� *
OrderItems+   
IsReadOnly 5v      �� # [ $ EditOrderForm��   Columns 5w      0  *System.Windows.Controls.DataGridTextColumn  5x      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Item Name��6%      Width5	  /System.Windows.Controls.DataGridLengthConverter$ 4*	 68   1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 5y   %   �� *Item.ItemName+  5z      $	 Qty��6%   $ *	 62    5{   %   �� *Qty+  5|      $
 Rate��6%   $ *	 63    5}   %   �� *AdjustedRate+  5~      $ Total��6%   $ *	 64    5   -   �� # [  5�      5�      5�      