   M S B A M L   `   `   `  ����B  >Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Manager.PagesManager.Pages  5   	   -clr-namespace:Manager.PagesManager.Pages  5   	       Manager.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   +localclr-namespace:Manager.Pages    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -pagesclr-namespace:Manager.Pages     5   	     � Title$  
MainWindow��5   	   $	��400��6   $	��400��6)    � WindowState$ 	Maximized=�65   $��SegoeUI)�5   	   $��20(�6   .��5      �  � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      �� 5      $��30 �6   .+�5   
   o� $
��Auto��6   $��20��6   $	��Top=�6(   .@�5      m� $��Manager��6   .@�5      m� $��Select Client��6   m� 5      $��
New Client��6   �� 5      m� 5      ��5      ]� 5      .+�5      �� Exit6.   �� 5      $��10��6(   65   �� 5      $��Right=�6(   F26H   5      5      5      5      � 5   
   $��1��6   $��0, 10, 0, 0q�6    � ColumnDefinitions 5       �� 5!      $��* �6#   �� 5"      $	��250 �6#   .+�5'      � -   5         �Name$ ContentArea��5(      $��10, 0, 10, 0q�5'      5*      � 5,      $��1��6   ���#1582a1 5-      �� 5.      $
��Auto �6$   �� 5/      �� 50      $
��Auto �6$   .+�52      ��  �� Orientation$ 
Horizontal=�6   $��10q�66   .+�53      �� -   5         ��Name$ 
but_GoBack��53      $
��Back��55      56      �� 57      -   5         ��Name$ SideContentArea��59      $��10q�57      $��1��58      5:      �� 5;      -   5        $ NavContentArea��5<      $��2��5;      $��10q�6*   5>      5?      5@      � 5B   
   $��2��6    5C      �� 5D      �� 5E      $
��Auto �6#   .+�5G      �� $��30��6H   
�������$��16(�5I      ��5G      �� *AppStatus.StatusBarColor+.@�5J      �� ��6   �� *AppStatus.AppStatusMessage+6J   5K      �� 5L      $��1��6   $��Assets/infigrail.png��5M      $��Uniform=�5N      6(   5O      5P      5Q      