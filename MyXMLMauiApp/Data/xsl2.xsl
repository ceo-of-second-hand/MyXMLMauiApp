<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="html" indent="yes"/>

	<xsl:template match="/ClothingPieces">
		<html>
			<head>
				<style>
					body {
					font-family: Arial, sans-serif;
					margin: 20px;
					background-color: #ffccff; /* Light pink background */
					}
					h1 {
					color: #333;
					text-align: center;
					}
					table {
					width: 80%;
					margin: 20px auto;
					border-collapse: collapse;
					background-color: #fff;
					}
					th, td {
					padding: 10px;
					border: 1px solid #ddd;
					text-align: center;
					}
					th {
					background-color: #000; /* Black header row */
					color: white;
					}
					tr:nth-child(even) {
					background-color: #f2f2f2;
					}
				</style>
			</head>
			<body>
				<h1>Clothing Catalog</h1>
				<table>
					<tr>
						<th>Brand</th>
						<th>Release Year</th>
						<th>Color Scheme</th>
						<th>Type of Piece</th>
					</tr>
					<xsl:for-each select="ClothingPiece">
						<tr>
							<td>
								<xsl:value-of select="@Brand"/>
							</td>
							<td>
								<xsl:value-of select="@ReleaseYear"/>
							</td>
							<td>
								<xsl:value-of select="@ColorScheme"/>
							</td>
							<td>
								<xsl:value-of select="@TypeOfPiece"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
