   M S B A M L   `   `   `  ����J  FWpfApplication3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication3WpfApplication3  5   	   H DMahApps.Metro, Version=1.6.1.4, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	   &     WpfApplication3.PrintQueueWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication3     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   -   5        $��0q�5	   	   (  "MahApps.Metro.Controls.MetroWindow    	GlowBrush
  �   � � 
ResizeMode$ CanResizeWithGrip=�5   	      WindowTransitionsEnabled .   � WindowStartupLocation$ CenterScreen=�5   	   ���#333843   TitleAlignment$ Center=�5   	    � Title$  ��5   	   $	��325��6   $	��400��6   .��5      �  � RowDefinitions 5   
   �� 5      $	��35* �6   �� 5      $
��265* �6   �� 5      $	��25* �6   .+�5   
   � $��0��6   ���#3897C5 � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��4* �6#   �� 5      $��6* �6#   �� 5      $��4* �6#   �� 5      $��* �6#   .+�5!      �� -   5         ��Name$ 
queueTitle��5!   (   $��2��6   $��Printing Queue��6<      DefaultBlock#��[  6R   $��Center=�6x   $��Center=�6�   ���#3338436�   �� 5"      -   5        $��1��5"      $��pictures/downloadicon.png��6$   $��Center=�6G   $��20��6d   $��Center=�6o   6�   �� 5#      $��3��6   $��pictures/stopicon.png��6$   $��Center=�6C   $��30��6`   $��Center=�6k   5$      �� 5%   
   $��1��6   $��15q�6   
�������	 �� CornerRadius$	 4,4,4,4��6<   ._�5&      �� -   5        
 ��Name$
 itemlist��5&      $��5q�6(   
������� � 	Resources 5'       �� 
TargetType �� Property �� Value��%�   '��       �� 5(      " ��6   .�5)      ��  � ��$ 
10,10,10,0q�63   �� 5*       � ��
 �   �67   �� 5+       � ��$ Berlin Sans FB)�67   �� 5,       � ��$ 25(�65   �� 5-       � ��$ Light"�67   5.      5/      50      51      �� 52   
   
�������$��2��6$   $��
15,0,15,10q�61   $	 2��6E   ._�53      � -   5         �Name$ pb��53      $��1q�6&   
�������$��0q�6D   54      55      56      