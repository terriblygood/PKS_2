   M S B A M L   `   `   `  ����B  >Network, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:NetworkNetwork  5   	       Network.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Network     5   	   
�������  � Title$  Окно��5   	   $	��775��6   $
��1000��6#   .��5	      � $��15q�6   
������� � ColumnDefinitions 5
   
   �� 5      $	��41* �6   �� 5      $	��21* �6   �� 5      $
��327* �6   �� 5      $
��582* �6    � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��2* �6   .+�5   
   �� $)��#Сетевые интерфейсы��6   $��0��6/   $��0��6<   $��
5,10,5,133q�6L   $��3��6`   .��5      �� -   5        $��
Interfaces��5      $��0,0,0,57q�6W   5      �� 5   
   $4��.Информация об интерфейсе��6   $��0��65   $��3��6B   $��
5,10,5,192q�6R   .��5      �� -   5        $��InterfaceDetails��5   ,   
�������$��0,0,10,0q�6D   $	��214��6V   5      5      �� 5   
   $��Анализ URL/URI��6   $��4��6+   $��0,273,10,115q�6?   $��2��6U   .��5      �� $��5,0,5,0q�6   $	��283��6*   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0,0,0,5q�66   .+�5      �� -   5        $��
tbUrlInput��5      $	��400��60   $��0,0,10,0q�6<   �� 5      -   5        $ ��Анализировать��5      65   5       �� 5!      -   5         ��Name$ tbUrlResults��5!      $
��Wrap=�62   $	��264��6F   
������� �� Padding$ 5q�5"   3   5#      5$      �� 5%   
   $��История URL��6   $��4��6(   $��0,146,32,10q�6<   $��1��6Q   .��5&      �� -   5         ��Name$ lbUrlHistory��5&      $��79��6,   5'      5(      5)      