   M S B A M L   `   `   `  ����E  AWPFSandbox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:WPFSandbox
WPFSandbox  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       WPFSandbox.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:WPFSandbox     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$  
MainWindow��5	   	   $	��360��6   $	��480��6)    � 
ResizeMode$ CanMinimize=�65    � WindowStyle$ SingleBorderWindow=�5
   	    � 	Resources 5       
  StrKey 
 CbxArr �� Type��%�   &	        &	       ��5   
   StrValue��6.   �� 5   
   " ��6!   .s�5      ��	item1��6    ��5      	item2��6    ��5      	item3��6    5      5      .��5      �� .+�5   
   � $	��248��6   $	��400��6   $��0q�6(   #��� ��63    �� Left$ 38��6z   
 �� Top$ 34��6�   .+�5      �� -   5         ��Name$ 
tabControl��5      $��Center=�6-   $	��228��6J   $��10q�6W   $��Center=�6c   $	��380��6~   .@�5      �� $��Login��6   .��5      � ���	#FFE5E5E5 � RowDefinitions 5      �� 5      $	��60* �6,   �� 5      $	��11* �6,   �� 5      $
��123* �6,   	 � ColumnDefinitions	 5       �� 5!      $
��147* �6/   �� 5"      $
��223* �6/   .+�5$      �� -   5        
 ��Name$
 lblUsername��5$       $��Username��65   $��Bottom=�6H   $��28��6c    J� RenderTransformOrigin$ 0.942,0.393&�6o   $��Right=�6�   $��69��6�   �� 5%      -   5        $
 lblPassword��5%       $��Password��65   $��Right=�6H   $��65��6d   $��2��6o   $ 0.615,0.714&�6|   $��28��6�   $	��Top=�6�   �� 5&      -   5         ��Name$ tbxUsername��5&   "   $��1��67   $
��Left=�6G   $��28��6b    �� TextWrapping$
 Wrap=�6n   $��Bottom=�6�   $	��120��6�   .��6�    � ToolTip 5'      _� 5(   "   .��6*   asd6/   .��5)   /   5*      G� 5+      -   5         G�Name$ tbxPassword��5+   &   $��1��6;   $
��Left=�6K   $��2��6f   $	��Top=�6s   $	��120��6�   $��28��6�   �� 5,      -   5         ��Name$ btnLogin��5,   !   $��Login��63   $
��Left=�6C   $��0,66,0,0q�6^   $��2��6p   $	��Top=�6}   $��75��6�   $��1��6�   $��24��6�   6�   5.      5/      �� 50      $��Signup��6   .��51      � ���	#FFE5E5E56   52      53      54      55      58      