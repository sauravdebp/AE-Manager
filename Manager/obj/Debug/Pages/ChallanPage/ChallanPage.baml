   M S B A M L   `   `   `  ����B  >Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Manager.PagesManager.Pages  5      c6clr-namespace:Manager.UserControls.ChallanPageControls(Manager.UserControls.ChallanPageControls  5      K*clr-namespace:Manager.Utilities.ConvertersManager.Utilities.Converters  5          Manager.Pages.ChallanPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      +localclr-namespace:Manager.Pages    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      RchallanControls6clr-namespace:Manager.UserControls.ChallanPageControls     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      A
converters*clr-namespace:Manager.Utilities.Converters     5        � 	Resources  5      8   2Manager.Utilities.Converters.ObjectStatusConverter   VisibilityConverter9   3Manager.Utilities.Converters.InWordsAmountConverter  InWordsAmountConverter��%:   &	        &	        5   
   6+     5   
   6,   5      .��5      � -   5         �Name$ 
grid_Outer��5      .+�5   
   ,� .��5      /� -   5         /�Name$	 doc��5       /� ColumnWidth 5      ��  �� ElementName$ 
grid_Outer�� �� Path$ ActualWidth� �� Mode$ OneWay=�.T�5      �� .r�5      � -   5        $ grid_Challan��5       � RowDefinitions 5      �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5      $
��Auto �6,   �� 5       $
��Auto �6,   �� 5!      $
��Auto �6,   .+�5$      <   6Manager.UserControls.ChallanPageControls.ChallanHeader     OrderChallan 5%      �� *OrderChallan+	 �� RelativeSource	 �� 
 �� Mode$
 FindAncestor=� �� AncestorType$ UserControlL�A   ;Manager.UserControls.ChallanPageControls.ChallanInfoControl  5'      $��1��5(         OrderChallan 5)      �� *OrderChallan+	 �� $
 FindAncestor=�$ UserControlL�G   AManager.UserControls.ChallanPageControls.ChallanOrderItemsControl  5+      $��2��5,   -      	OrderForm 5-   -   �� *OrderChallan.OrderForm+	 �� $
 FindAncestor=�$ UserControlL�� 5/      $��3��6    � ColumnDefinitions 50      �� 51   "   $��2* �63   �� 52   "   $��* �63   �� 53   "   $��* �63    55      �� 56   "   �� 57   "   �� 58   "   �� 59   "   �� 5:   "   �� 5;   "   .+�5=      � $��3��6#    5>   "   �� 5?   &   �� 5@   &   .+�5B   "   �� $��
1, 1, 1, 0q�6)   
���   �$��2��5D      �� 5E   "   $��3q�6,   Rs. (In Words)6G   �� 5F   "   $��1��5G       $
��Wrap=�5H        �� TextAlignment$
 Left=�5I       $��3q�5J       ��5F   ,   �� *OrderChallan.GrandTotal+ �� 	Converter# [ 5J       5K      � 5L      $��1��6#   $��2��63    5M   "   �� 5N   &   �� 5O   &   .+�5Q   "   �� $��
0, 1, 1, 0q�6)   
���   �$��2��5R   =   �� 5S   "   $��3q�6,   VAT6<   �� 5T   "   $��1��6,   $��3q�5V   ,   ��5U   ,   �� *OrderChallan.Vat+5V   ,   5W      � 5X      $��1��6#   $��1��5Y   #   $��2��60    5Z   "   �� 5[   &   �� 5\   &   .+�5^   "   �� $��
0, 1, 1, 0q�6)   
���   �$��2��5_   =   �� 5`   "   $��3q�6,   CST6<   �� 5a   "   $��1��6,   $��3q�5c   ,   ��5b   ,   �� *OrderChallan.Cst+5c   ,   5d      � 5f      $��1��6#   $��2��5g   #   $��2��60    5h   "   �� 5i   &   �� 5j   &   .+�5l   "   �� $��
0, 1, 1, 0q�6)   
���   �$��2��5m   =   �� 5n   "   $��3q�6,   	Sub Total6B   �� 5o   "   $��1��6,   $��3q�5q   ,   ��5p   ,   �� *OrderChallan.SubTotal+5q   ,   5r      � 5t      $��3��6#   $��2��5u   #    5v   "   �� 5w   &   �� 5x   &   .+�5z   "   �� $��1q�6)   
���   �$��2��5|   )   �� 5}   "   $��3q�6,   Special Note (if any):6O   �� 5~   "   $��1��6,   ��5   ,   �� *OrderChallan.SpecialNote+5�      � 5�      $��1��6#   $��3��5�   #   $��2��60    5�   "   �� 5�   &   �� 5�   &   .+�5�   "   �� $��
0, 1, 1, 0q�6)   
���   �$��2��5�   )   �� 5�   "   $��3q�6,   	Freight6@   �� 5�   "   $��1��6,   ��5�   ,   �� *OrderChallan.FreightCharge+5�      � 5�      $��1��6#   $��4��5�   #   $��2��60    5�   "   �� 5�   &   �� 5�   &   .+�5�   "   �� $��
0, 1, 1, 1q�6)   
���   �$��2��5�   =   �� 5�   "   $��3q�6,   Grand Total Rounded6L   �� 5�   "   $��1��6,   $��3q�5�   ,   ��5�   ,   �� *OrderChallan.GrandTotal+5�   ,   5�      � 5�      $��3��6#   $��5��5�   3    5�   "   �� 5�   &   �� 5�   &    5�   "   �� 5�   &   �� 5�   &   �� 5�   &   �� 5�   &   �� 5�   &   �� 5�   &   .+�5�   "   � $��2��6'   $��50��68   .+�5�   &   �� $��
1, 0, 1, 1q�6-   
���   �5�   -   �� 5�   &   $��3q�60   Pre-Authentication6O   5�   #   � 5�   "   $��2��6'   .+�5�   &   �� $��
1, 0, 1, 1q�6-   
���   �5�   -   �� 5�   &   $��3q�60    NAME: KISHOLOY DEB PURKAYASTHA6[   5�   #   � 5�   "   $��3��6'   .+�5�   &   �� $��
1, 0, 1, 1q�6-   
���   �5�   -   �� 5�   &   $��3q�60    STATUS: PROPRIETOR, 98100783906[   5�   #   � 5�   "   $��1��6'   $��4��5�   '   .+�5�   &   �� $��
0, 0, 1, 1q�6-   
���   �5�   -   �� 5�   &   $��3q�60   for Aquatek Engineers6R   5�   #   � 5�   "   $��4��6'   .+�5�   &   �� $��
1, 0, 0, 0q�6-   
���   �5�   -   �� 5�   &   $
��Wrap=�60   $��3q�5�   0   ECReceived in good condition as per quantity and quality agreed upon.5�   '   5�   #   � 5�   "   $��5��6'   .+�5�   &   �� $��
1, 0, 0, 1q�6-   
���   �5�   -   �� 5�   &   $��3q�60   DATE :6C   5�   #   � 5�   "   $��1��6'   $��4��5�   '   $��2��5�   '   .+�5�   &   �� $��
0, 0, 1, 1q�6-   
���   �5�   -   �� 5�   &   $��3q�60   $��Bottom=�5�   0   $��Right=�5�   0   Signature of Purchaser6d   5�   #   5�      5�      <   6Manager.UserControls.ChallanPageControls.ChallanFooter  5�      $��4��68   5�      5�      5�      5�      5�      5�      