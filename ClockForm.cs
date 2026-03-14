using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SaatUygulamasi
{
    public class ClockForm : Form
    {
        private Timer timer;
        private DateTime currentTime;
        private ComboBox countryCombo;
        private string selectedTimeZone = "Turkey Standard Time";

        private readonly Color bgColor = Color.FromArgb(15, 15, 25);
        private readonly Color faceColor = Color.FromArgb(22, 22, 38);
        private readonly Color accentColor = Color.FromArgb(80, 200, 255);
        private readonly Color secondColor = Color.FromArgb(255, 80, 100);
        private readonly Color textColor = Color.FromArgb(220, 230, 255);
        private readonly Color dimColor = Color.FromArgb(60, 65, 90);
        private readonly Color comboBack = Color.FromArgb(28, 28, 45);

        private readonly List<(string Name, string TZ)> countries = new List<(string, string)>
        {
            ("Türkiye", "Turkey Standard Time"),
            ("Afganistan", "Afghanistan Standard Time"),
            ("Arnavutluk", "Central Europe Standard Time"),
            ("Cezayir", "W. Central Africa Standard Time"),
            ("Andorra", "Romance Standard Time"),
            ("Angola", "W. Central Africa Standard Time"),
            ("Antigua ve Barbuda", "SA Western Standard Time"),
            ("Arjantin", "Argentina Standard Time"),
            ("Ermenistan", "Caucasus Standard Time"),
            ("Avustralya (Sydney)", "AUS Eastern Standard Time"),
            ("Avustralya (Perth)", "W. Australia Standard Time"),
            ("Avustralya (Darwin)", "AUS Central Standard Time"),
            ("Avustralya (Adelaide)", "Cen. Australia Standard Time"),
            ("Avustralya (Brisbane)", "E. Australia Standard Time"),
            ("Avusturya", "W. Europe Standard Time"),
            ("Azerbaycan", "Azerbaijan Standard Time"),
            ("Bahamalar", "Eastern Standard Time"),
            ("Bahreyn", "Arab Standard Time"),
            ("Bangladeş", "Bangladesh Standard Time"),
            ("Barbados", "SA Western Standard Time"),
            ("Belarus", "Belarus Standard Time"),
            ("Belçika", "Romance Standard Time"),
            ("Belize", "Central America Standard Time"),
            ("Benin", "W. Central Africa Standard Time"),
            ("Butan", "Bangladesh Standard Time"),
            ("Bolivya", "SA Western Standard Time"),
            ("Bosna Hersek", "Central Europe Standard Time"),
            ("Botsvana", "South Africa Standard Time"),
            ("Brezilya (Brasilia)", "E. South America Standard Time"),
            ("Brezilya (Manaus)", "SA Western Standard Time"),
            ("Brunei", "Singapore Standard Time"),
            ("Bulgaristan", "FLE Standard Time"),
            ("Burkina Faso", "Greenwich Standard Time"),
            ("Burundi", "South Africa Standard Time"),
            ("Yeşil Burun Adaları", "Cape Verde Standard Time"),
            ("Kamboçya", "SE Asia Standard Time"),
            ("Kamerun", "W. Central Africa Standard Time"),
            ("Kanada (Toronto)", "Eastern Standard Time"),
            ("Kanada (Vancouver)", "Pacific Standard Time"),
            ("Kanada (Calgary)", "Mountain Standard Time"),
            ("Kanada (Winnipeg)", "Central Standard Time"),
            ("Orta Afrika Cumhuriyeti", "W. Central Africa Standard Time"),
            ("Çad", "W. Central Africa Standard Time"),
            ("Şili", "Pacific SA Standard Time"),
            ("Çin", "China Standard Time"),
            ("Kolombiya", "SA Pacific Standard Time"),
            ("Komorlar", "E. Africa Standard Time"),
            ("Kongo", "W. Central Africa Standard Time"),
            ("Kosta Rika", "Central America Standard Time"),
            ("Hırvatistan", "Central Europe Standard Time"),
            ("Küba", "Cuba Standard Time"),
            ("Kıbrıs", "GTB Standard Time"),
            ("Çek Cumhuriyeti", "Central Europe Standard Time"),
            ("Danimarka", "Romance Standard Time"),
            ("Cibuti", "E. Africa Standard Time"),
            ("Dominika", "SA Western Standard Time"),
            ("Dominik Cumhuriyeti", "SA Western Standard Time"),
            ("Doğu Timor", "Tokyo Standard Time"),
            ("Ekvador", "SA Pacific Standard Time"),
            ("Mısır", "Egypt Standard Time"),
            ("El Salvador", "Central America Standard Time"),
            ("Ekvator Ginesi", "W. Central Africa Standard Time"),
            ("Eritre", "E. Africa Standard Time"),
            ("Estonya", "FLE Standard Time"),
            ("Esvatini", "South Africa Standard Time"),
            ("Etiyopya", "E. Africa Standard Time"),
            ("Fiji", "Fiji Standard Time"),
            ("Finlandiya", "FLE Standard Time"),
            ("Fransa", "Romance Standard Time"),
            ("Gabon", "W. Central Africa Standard Time"),
            ("Gambiya", "Greenwich Standard Time"),
            ("Gürcistan", "Georgian Standard Time"),
            ("Almanya", "W. Europe Standard Time"),
            ("Gana", "Greenwich Standard Time"),
            ("Yunanistan", "GTB Standard Time"),
            ("Grenada", "SA Western Standard Time"),
            ("Guatemala", "Central America Standard Time"),
            ("Gine", "Greenwich Standard Time"),
            ("Gine-Bissau", "Greenwich Standard Time"),
            ("Guyana", "SA Western Standard Time"),
            ("Haiti", "Haiti Standard Time"),
            ("Honduras", "Central America Standard Time"),
            ("Macaristan", "Central Europe Standard Time"),
            ("İzlanda", "Greenwich Standard Time"),
            ("Hindistan", "India Standard Time"),
            ("Endonezya (Cakarta)", "SE Asia Standard Time"),
            ("Endonezya (Makassar)", "Singapore Standard Time"),
            ("Endonezya (Jayapura)", "Tokyo Standard Time"),
            ("İran", "Iran Standard Time"),
            ("Irak", "Arabic Standard Time"),
            ("İrlanda", "GMT Standard Time"),
            ("İsrail", "Israel Standard Time"),
            ("İtalya", "W. Europe Standard Time"),
            ("Jamaika", "SA Pacific Standard Time"),
            ("Japonya", "Tokyo Standard Time"),
            ("Ürdün", "Jordan Standard Time"),
            ("Kazakistan (Almatı)", "Central Asia Standard Time"),
            ("Kenya", "E. Africa Standard Time"),
            ("Kiribati", "UTC+12"),
            ("Kuzey Kore", "North Korea Standard Time"),
            ("Güney Kore", "Korea Standard Time"),
            ("Kosova", "Central Europe Standard Time"),
            ("Kuveyt", "Arab Standard Time"),
            ("Kırgızistan", "Central Asia Standard Time"),
            ("Laos", "SE Asia Standard Time"),
            ("Letonya", "FLE Standard Time"),
            ("Lübnan", "Middle East Standard Time"),
            ("Lesoto", "South Africa Standard Time"),
            ("Liberya", "Greenwich Standard Time"),
            ("Libya", "Libya Standard Time"),
            ("Liechtenstein", "W. Europe Standard Time"),
            ("Litvanya", "FLE Standard Time"),
            ("Lüksemburg", "W. Europe Standard Time"),
            ("Madagaskar", "E. Africa Standard Time"),
            ("Malavi", "South Africa Standard Time"),
            ("Malezya", "Singapore Standard Time"),
            ("Maldivler", "West Asia Standard Time"),
            ("Mali", "Greenwich Standard Time"),
            ("Malta", "W. Europe Standard Time"),
            ("Marşal Adaları", "UTC+12"),
            ("Moritanya", "Greenwich Standard Time"),
            ("Mauritius", "Mauritius Standard Time"),
            ("Meksika (Mexico City)", "Central Standard Time (Mexico)"),
            ("Meksika (Tijuana)", "Pacific Standard Time (Mexico)"),
            ("Mikronezya", "West Pacific Standard Time"),
            ("Moldova", "GTB Standard Time"),
            ("Monako", "Romance Standard Time"),
            ("Moğolistan", "Ulaanbaatar Standard Time"),
            ("Karadağ", "Central Europe Standard Time"),
            ("Fas", "Morocco Standard Time"),
            ("Mozambik", "South Africa Standard Time"),
            ("Myanmar", "Myanmar Standard Time"),
            ("Namibya", "Namibia Standard Time"),
            ("Nauru", "UTC+12"),
            ("Nepal", "Nepal Standard Time"),
            ("Hollanda", "W. Europe Standard Time"),
            ("Yeni Zelanda", "New Zealand Standard Time"),
            ("Nikaragua", "Central America Standard Time"),
            ("Nijer", "W. Central Africa Standard Time"),
            ("Nijerya", "W. Central Africa Standard Time"),
            ("Kuzey Makedonya", "Central Europe Standard Time"),
            ("Norveç", "W. Europe Standard Time"),
            ("Umman", "Arabian Standard Time"),
            ("Pakistan", "Pakistan Standard Time"),
            ("Palau", "Tokyo Standard Time"),
            ("Panama", "SA Pacific Standard Time"),
            ("Papua Yeni Gine", "West Pacific Standard Time"),
            ("Paraguay", "Paraguay Standard Time"),
            ("Peru", "SA Pacific Standard Time"),
            ("Filipinler", "Singapore Standard Time"),
            ("Polonya", "Central Europe Standard Time"),
            ("Portekiz", "GMT Standard Time"),
            ("Katar", "Arab Standard Time"),
            ("Romanya", "GTB Standard Time"),
            ("Rusya (Moskova)", "Russian Standard Time"),
            ("Rusya (Vladivostok)", "Vladivostok Standard Time"),
            ("Rusya (Yekaterinburg)", "Ekaterinburg Standard Time"),
            ("Ruanda", "South Africa Standard Time"),
            ("Saint Kitts ve Nevis", "SA Western Standard Time"),
            ("Saint Lucia", "SA Western Standard Time"),
            ("Saint Vincent", "SA Western Standard Time"),
            ("Samoa", "Samoa Standard Time"),
            ("San Marino", "W. Europe Standard Time"),
            ("Sao Tome ve Principe", "Greenwich Standard Time"),
            ("Suudi Arabistan", "Arab Standard Time"),
            ("Senegal", "Greenwich Standard Time"),
            ("Sırbistan", "Central Europe Standard Time"),
            ("Seyşeller", "Mauritius Standard Time"),
            ("Sierra Leone", "Greenwich Standard Time"),
            ("Singapur", "Singapore Standard Time"),
            ("Slovakya", "Central Europe Standard Time"),
            ("Slovenya", "Central Europe Standard Time"),
            ("Solomon Adaları", "Central Pacific Standard Time"),
            ("Somali", "E. Africa Standard Time"),
            ("Güney Afrika", "South Africa Standard Time"),
            ("Güney Sudan", "E. Africa Standard Time"),
            ("İspanya", "Romance Standard Time"),
            ("Sri Lanka", "Sri Lanka Standard Time"),
            ("Sudan", "E. Africa Standard Time"),
            ("Surinam", "SA Eastern Standard Time"),
            ("İsveç", "W. Europe Standard Time"),
            ("İsviçre", "W. Europe Standard Time"),
            ("Suriye", "Syria Standard Time"),
            ("Tayvan", "Taipei Standard Time"),
            ("Tacikistan", "West Asia Standard Time"),
            ("Tanzanya", "E. Africa Standard Time"),
            ("Tayland", "SE Asia Standard Time"),
            ("Togo", "Greenwich Standard Time"),
            ("Tonga", "Tonga Standard Time"),
            ("Trinidad ve Tobago", "SA Western Standard Time"),
            ("Tunus", "W. Central Africa Standard Time"),
            ("Türkmenistan", "West Asia Standard Time"),
            ("Tuvalu", "UTC+12"),
            ("Uganda", "E. Africa Standard Time"),
            ("Ukrayna", "FLE Standard Time"),
            ("Birleşik Arap Emirlikleri", "Arabian Standard Time"),
            ("İngiltere", "GMT Standard Time"),
            ("ABD (New York)", "Eastern Standard Time"),
            ("ABD (Chicago)", "Central Standard Time"),
            ("ABD (Denver)", "Mountain Standard Time"),
            ("ABD (Los Angeles)", "Pacific Standard Time"),
            ("ABD (Anchorage)", "Alaskan Standard Time"),
            ("ABD (Honolulu)", "Hawaiian Standard Time"),
            ("Uruguay", "Montevideo Standard Time"),
            ("Özbekistan", "West Asia Standard Time"),
            ("Vanuatu", "Central Pacific Standard Time"),
            ("Vatikan", "W. Europe Standard Time"),
            ("Venezuela", "Venezuela Standard Time"),
            ("Vietnam", "SE Asia Standard Time"),
            ("Yemen", "Arab Standard Time"),
            ("Zambiya", "South Africa Standard Time"),
            ("Zimbabve", "South Africa Standard Time"),
        };

        public ClockForm()
        {
            this.Text = "Dünya Saati";
            this.Size = new Size(420, 590);
            this.MinimumSize = new Size(320, 480);
            this.BackColor = bgColor;
            this.ForeColor = textColor;
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;

            countryCombo = new ComboBox();
            countryCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            countryCombo.BackColor = comboBack;
            countryCombo.ForeColor = textColor;
            countryCombo.FlatStyle = FlatStyle.Flat;
            countryCombo.Font = new Font("Segoe UI", 10f);
            countryCombo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            foreach (var c in countries)
                countryCombo.Items.Add(c.Name);

            countryCombo.SelectedIndex = 0;
            countryCombo.SelectedIndexChanged += (s, e) =>
            {
                selectedTimeZone = countries[countryCombo.SelectedIndex].TZ;
                Invalidate();
            };

            this.Controls.Add(countryCombo);

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += (s, e) => Invalidate();
            timer.Start();

            this.Resize += (s, e) => RepositionCombo();
            RepositionCombo();
        }

        private void RepositionCombo()
        {
            countryCombo.SetBounds(12, 10, ClientSize.Width - 24, 30);
        }

        private DateTime GetCurrentTime()
        {
            try
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(selectedTimeZone);
                return TimeZoneInfo.ConvertTime(DateTime.Now, tz);
            }
            catch { return DateTime.Now; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            currentTime = GetCurrentTime();

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int w = ClientSize.Width;
            int h = ClientSize.Height;
            int topOffset = 50;

            int availH = h - topOffset;
            int clockAreaH = (int)(availH * 0.65);
            int cx = w / 2;
            int cy = topOffset + clockAreaH / 2;
            int radius = Math.Min(w / 2, clockAreaH / 2) - 20;

            DrawAnalogClock(g, cx, cy, radius);
            DrawDigitalSection(g, 0, topOffset + clockAreaH, w, h - topOffset - clockAreaH);
        }

        private void DrawAnalogClock(Graphics g, int cx, int cy, int radius)
        {
            for (int i = 4; i >= 1; i--)
            {
                using var glowPen = new Pen(Color.FromArgb(15 * i, accentColor), i * 3);
                g.DrawEllipse(glowPen, cx - radius - 2, cy - radius - 2, (radius + 2) * 2, (radius + 2) * 2);
            }

            using var faceBrush = new SolidBrush(faceColor);
            g.FillEllipse(faceBrush, cx - radius, cy - radius, radius * 2, radius * 2);

            using var rimPen = new Pen(accentColor, 2f);
            g.DrawEllipse(rimPen, cx - radius, cy - radius, radius * 2, radius * 2);

            for (int i = 0; i < 60; i++)
            {
                double angle = i * 6.0 * Math.PI / 180.0;
                bool isHour = (i % 5 == 0);
                float innerR = isHour ? radius - 16 : radius - 10;
                float outerR = radius - 4;

                float x1 = cx + (float)(innerR * Math.Sin(angle));
                float y1 = cy - (float)(innerR * Math.Cos(angle));
                float x2 = cx + (float)(outerR * Math.Sin(angle));
                float y2 = cy - (float)(outerR * Math.Cos(angle));

                if (isHour)
                {
                    using var p = new Pen(accentColor, 2.5f);
                    g.DrawLine(p, x1, y1, x2, y2);

                    int hour = i / 5 == 0 ? 12 : i / 5;
                    float numR = radius - 30;
                    float nx = cx + (float)(numR * Math.Sin(angle));
                    float ny = cy - (float)(numR * Math.Cos(angle));
                    using var numFont = new Font("Segoe UI", radius * 0.09f, FontStyle.Bold);
                    using var numBrush = new SolidBrush(textColor);
                    string numStr = hour.ToString();
                    SizeF ns = g.MeasureString(numStr, numFont);
                    g.DrawString(numStr, numFont, numBrush, nx - ns.Width / 2, ny - ns.Height / 2);
                }
                else
                {
                    using var p = new Pen(dimColor, 1f);
                    g.DrawLine(p, x1, y1, x2, y2);
                }
            }

            double hourAngle = ((currentTime.Hour % 12) + currentTime.Minute / 60.0 + currentTime.Second / 3600.0) * 30.0 * Math.PI / 180.0;
            DrawHand(g, cx, cy, hourAngle, radius * 0.55f, 5f, accentColor);

            double minAngle = (currentTime.Minute + currentTime.Second / 60.0) * 6.0 * Math.PI / 180.0;
            DrawHand(g, cx, cy, minAngle, radius * 0.78f, 3.5f, textColor);

            double secAngle = (currentTime.Second + currentTime.Millisecond / 1000.0) * 6.0 * Math.PI / 180.0;
            DrawHand(g, cx, cy, secAngle, radius * 0.85f, 1.5f, secondColor, counterLen: radius * 0.2f);

            using var centerBrush = new SolidBrush(accentColor);
            g.FillEllipse(centerBrush, cx - 6, cy - 6, 12, 12);
            using var centerDot = new SolidBrush(secondColor);
            g.FillEllipse(centerDot, cx - 3, cy - 3, 6, 6);
        }

        private void DrawHand(Graphics g, int cx, int cy, double angle, float length, float width, Color color, float counterLen = 0)
        {
            float tipX = cx + (float)(length * Math.Sin(angle));
            float tipY = cy - (float)(length * Math.Cos(angle));

            using var glowPen = new Pen(Color.FromArgb(60, color), width * 3);
            glowPen.StartCap = LineCap.Round;
            glowPen.EndCap = LineCap.Round;

            if (counterLen > 0)
            {
                float cX = cx - (float)(counterLen * Math.Sin(angle));
                float cY = cy + (float)(counterLen * Math.Cos(angle));
                g.DrawLine(glowPen, cX, cY, tipX, tipY);
            }
            else g.DrawLine(glowPen, cx, cy, tipX, tipY);

            using var pen = new Pen(color, width);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;

            if (counterLen > 0)
            {
                float cX = cx - (float)(counterLen * Math.Sin(angle));
                float cY = cy + (float)(counterLen * Math.Cos(angle));
                g.DrawLine(pen, cX, cY, tipX, tipY);
            }
            else g.DrawLine(pen, cx, cy, tipX, tipY);
        }

        private void DrawDigitalSection(Graphics g, int x, int y, int w, int h)
        {
            using var sepPen = new Pen(Color.FromArgb(50, accentColor), 1);
            g.DrawLine(sepPen, x + 30, y + 1, x + w - 30, y + 1);

            int midX = x + w / 2;
            int midY = y + h / 2;

            string timeStr = currentTime.ToString("HH:mm:ss");
            using var bigFont = new Font("Consolas", h * 0.28f, FontStyle.Bold);
            using var bigBrush = new SolidBrush(accentColor);
            SizeF ts = g.MeasureString(timeStr, bigFont);
            g.DrawString(timeStr, bigFont, bigBrush, midX - ts.Width / 2, midY - ts.Height / 2 - 8);

            string dateStr = currentTime.ToString("dd MMMM yyyy, dddd", new System.Globalization.CultureInfo("tr-TR"));
            using var dateFont = new Font("Segoe UI", h * 0.1f, FontStyle.Regular);
            using var dateBrush = new SolidBrush(dimColor);
            SizeF ds = g.MeasureString(dateStr, dateFont);
            g.DrawString(dateStr, dateFont, dateBrush, midX - ds.Width / 2, midY + ts.Height / 2 - 4);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timer?.Stop();
            timer?.Dispose();
            base.OnFormClosed(e);
        }
    }
}