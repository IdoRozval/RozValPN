   M S B A M L   `   `   `  ����J  FWpfApplication3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication3WpfApplication3  5   	   H DMahApps.Metro, Version=1.6.1.4, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	   )    #WpfApplication3.CreateAccountWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication3     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   $��0q�5
   	   (  "MahApps.Metro.Controls.MetroWindow    	GlowBrush
  �   � � 
ResizeMode$ CanResizeWithGrip=�5   	      WindowTransitionsEnabled .   � WindowStartupLocation$ CenterScreen=�5   	    � Title$ Create Account��5   	   $	��400��6    $	��600��6-      TitleAlignment$ Center=�69      TitleForeground
 �����.��5      � .+�5   
   �� $��pictures/7_4_2500.png��6   $
��Fill=�6/   �� 5   
   $��0,0,0,0q�6   $��Center=�6"   $��Center=�6=   $	��300��6Z   $	��325��6f    �� CornerRadius$ 10,10,10,10��6s   ���	#FFA73A26._�5      � ���	#FFA73A26$��5q�6+    � RowDefinitions 5      �� 5      $��* �6$   �� 5      $��* �6$   �� 5      $��* �6$   �� 5      $��* �6$   �� 5      $��* �6$   .+�5       �� $��0��6   $��Assign Details��6)   $��Rockwell)�6?   $��35(�6U   	 �� TextAlignment$	 Center=�6c   $��Center=�6z   
�������6�   �� 5"      -   5        
 ��Name$
 userbox��5"      
�������$��1��6>   $	��250��6K   $��40��6W   $��Username��6c   $��0,0,0,1q�6s   ���	#FFA73A26$��Rockwell)�6�   $��25(�6�    �� TextAlignment$ Center=�6�   $��	10,0,10,0q�6�   6
  �� 5$      -   5        $
 passbox��5$      
�������$��2��6>   $	��250��6K   $��40��6W   $��Password��6c   $��0,0,0,1q�6s   ���	#FFA73A26$��Rockwell)�6�   $��25(�6�   $ Center=�6�   $��	10,0,10,0q�6�   6
  �� 5&      -   5        $
 emailbox��5&      
�������$��3��6?   $	��250��6L   $��40��6X   $��Email��6d   $��0,0,0,1q�6q   ���	#FFA73A26$��Rockwell)�6�   $��25(�6�   $ Center=�6�   $��	10,0,10,0q�6�   6	  �� 5'      $��4��6   ���#ffc300$��Center=�6;   $��Center=�6V   $ 10,10,10,10��6s   ._�5(      �� -   5        $	��125��5(   Y   ���#ffc300$��5q�6z   $��Create��6�   $��Rockwell)�6�   $��22(�6�   6�   5)      5.      5/      54      55      