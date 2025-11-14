<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:t="http://microsoft.com/schemas/VisualStudio/TeamTest/2010">
  <xsl:output method="html" indent="yes" encoding="utf-8"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>SauceDemo Test Report</title>
        <style>
          body { font-family: 'Segoe UI', sans-serif; margin: 20px; background: #f4f6f9; color: #333; }
          h1 { color: #2c3e50; text-align: center; }
          .summary { background: #fff; padding: 20px; border-radius: 12px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); margin-bottom: 20px; }
          table { width: 100%; border-collapse: collapse; background: white; box-shadow: 0 2px 10px rgba(0,0,0,0.1); border-radius: 12px; overflow: hidden; }
          th { background: #3498db; color: white; padding: 15px; text-align: left; }
          td { padding: 12px 15px; border-bottom: 1px solid #eee; }
          .passed { background-color: #d5f5e3; }
          .failed { background-color: #fadbd8; font-weight: bold; }
          .duration { text-align: right; font-family: monospace; }
          pre { background: #f1f1f1; padding: 10px; border-radius: 6px; margin: 5px 0; white-space: pre-wrap; font-size: 0.9em; max-height: 200px; overflow-y: auto; }
          .logo { text-align: center; margin: 20px 0; }
          .logo img { height: 60px; }
        </style>
      </head>
      <body>
        <div class="logo">
          <img src="https://www.saucedemo.com/static/media/sauce-logo.9e6d5c0c.svg" alt="Swag Labs" />
        </div>
        <h1>SauceDemo Automation Test Report</h1>

        <!-- Summary -->
        <div class="summary">
          <p>
            <strong>Total Tests:</strong>
            <xsl:value-of select="count(//t:UnitTestResult)"/>
          </p>
          <p>
            <strong>Passed:</strong>
            <xsl:value-of select="count(//t:UnitTestResult[@outcome='Passed'])"/>
          </p>
          <p>
            <strong>Failed:</strong>
            <xsl:value-of select="count(//t:UnitTestResult[@outcome='Failed'])"/>
          </p>
          <p>
            <strong>Run Time:</strong>
            <xsl:value-of select="//t:Times/@finish"/> (UTC+2)
          </p>
        </div>

        <!-- Results Table -->
        <table>
          <tr>
            <th>Test Name</th>
            <th>Browser</th>
            <th>Outcome</th>
            <th class="duration">Duration</th>
            <th>Logs</th>
          </tr>
          <xsl:for-each select="//t:UnitTestResult">
            <tr>
              <xsl:attribute name="class">
                <xsl:if test="@outcome='Passed'">passed</xsl:if>
                <xsl:if test="@outcome='Failed'">failed</xsl:if>
              </xsl:attribute>
              <td>
                <xsl:value-of select="@testName"/>
              </td>
              <td>
                <xsl:choose>
                  <xsl:when test="contains(@testName, 'Firefox')">Firefox</xsl:when>
                  <xsl:when test="contains(@testName, 'Edge')">Edge</xsl:when>
                  <xsl:otherwise>—</xsl:otherwise>
                </xsl:choose>
              </td>
              <td>
                <xsl:value-of select="@outcome"/>
              </td>
              <td class="duration">
                <xsl:value-of select="@duration"/>
              </td>
              <td>
                <xsl:if test="t:Output/t:StdOut">
                  <pre>
                    <xsl:value-of select="t:Output/t:StdOut" disable-output-escaping="yes"/>
                  </pre>
                </xsl:if>
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>