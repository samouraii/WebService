   M S B A M L   `   `   `  ����I  EWpfApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfApplicationWpfApplication  5   	       WpfApplication.achat   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:WpfApplication     5   	   -   5          � Title$  achat��5   	   $��486.811��6   $��443.878��6(   .��5	      � -   5         �Name$ rr��5	      .+�5
   
   � -   5        $ 
gridChoice��5
      $y�Hidden=�6$   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ choice��5      $
��Left=�6'   $��0,7,0,0q�6B      AutoGenerateColumns .  $	��Top=�6�   $	��449��6�   $	��436��6�      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6�   ��6S   ��  �� Source$ WcfClient.Client��   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ id��6)      Width$
 Auto 6L   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 65   �� *id+6L     5      $ 
numdossier��6)   	 6=   �� *
numdossier+  5      $	 Nom��6)   	 66   �� *nom+  5      $ Numéro Tva��6)   	 6=   �� *numtva+5      5      � 5   
   -   5        $ 
gridInsert��5      $y�Hidden=�6#   .+�5      �� -   5        
 ��Name$
 label��5      $
��TVA:��6#   $
��Left=�62   $��	10,49,0,0q�6M   $	��Top=�6`    J� RenderTransformOrigin$ 0.457,-0.251&�6x   $��70��6�   �� 5      -   5        $
 label2��5      $��Prix hors tva:��6$   $
��Left=�6=   $��	10,80,0,0q�6X   $	��Top=�6k   $ 0.457,-0.251&�6�   $��70��6�   �� 5      -   5        $
 label3��5      $��Voiture:��6$   $
��Left=�67   $��
10,106,0,0q�6R   $	��Top=�6f   $ 0.457,-0.251&�6~   $��70��6�   �� 5      -	   5         ��Name$ textBox��5      $
��Left=�6'   $��23��6B   $��	96,52,0,0q�6N    �� TextWrapping$
 Wrap=�6a   $	��Top=�6�   $	��120��6�   ��6u   �� *tva+6�   �� 5      -
   5        $ textBox1��5      $
��Left=�6(   $��23��6C   $��	96,84,0,0q�6O   $
 Wrap=�6b   $	��Top=�6�   $	��120��6�   ��6v   �� *	prixhtv+6�   �� 5      -   5        $ textBox2��5      $
��Left=�6(   $��87��6C   $��
96,112,0,0q�6O   $
 Wrap=�6c   $	��Top=�6�   $	��307��6�   ��6w   �� *	voiture+6�   �� 5      -   5         ��Name$ button��5      $��valider��6%   $
��Left=�67   $��
89,254,0,0q�6R   $	��Top=�6f   $��75��6~   6�   �� 5      -   5        $ button1��5      $��	supprimer��6&   $y�Hidden=�6:   $
��Left=�6N   $��211,254,0,0q�6i   $	��Top=�6~   $��75��6�   6�   �� 5      -   5        $
 label1��5      $��Prix:��6$   $
��Left=�64   $��
10,214,0,0q�6O   $	��Top=�6c   $ 0.437,-0.144&�6{   �� 5      -   5        $

 prix��5      $��Label��6"   $
��Left=�62   $��
96,214,0,0q�6M   $	��Top=�6a   5"      � 5$   
   -   5        $ gridhistorique��5$      $y�Hidden=�6(   .+�5%        -   5        $ histo��5%      $
��Left=�6&   $��0,7,0,0q�6A    .  $	��Top=�6�   $	��449��6�   $	��436��6�   $ * 6  ��6R   �� $ WcfClient.Transaction�� 5&        5'      $ id��6)   $
 Auto 6L   	 65   �� *id+6L     5(      $	 tva��6)   	 66   �� *tva+  5)      $ Montant��6)   	 6:   �� *	prixhtv+  5*      $ voiture��6)   	 6:   �� *	voiture+5-      5.      5/      51      