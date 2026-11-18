# Trade History Calendar

The design shows a calendar grid for each month: **4 months** (September–December) across **6 years** (2019–2024). All data comes in a single response, so the frontend never needs to make additional queries.


## Data structure example

```jsonc
{
  "Start": "2019-09-01",
  "Months": [
    { "Length": 30, "FirstDay": 0 },   // Sep 2019, 1st is Sunday
    { "Length": 31, "FirstDay": 2 },   // Oct 2019, 1st is Tuesday
    { "Length": 30, "FirstDay": 5 }    // Nov 2019, 1st is Friday
    // …
  ],
  "Days": [
    { "Offset": 37, "Positions": 3, "Profit": 719.06,    "ProfitPercent": … },  // Oct 8
    { "Offset": 41, "Positions": 1, "Profit": 53.98,     "ProfitPercent": … },  // Oct 12
    { "Offset": 43, "Positions": 2, "Profit": -1661.05,  "ProfitPercent": … },  // Oct 14
    { "Offset": 52, "Positions": 1, "Profit": -2396.56,  "ProfitPercent": … },  // Oct 23
    { "Offset": 55, "Positions": 1, "Profit": 70828.11,  "ProfitPercent": … }   // Oct 26
  ]
}
```

## How the frontend renders a month

The response is a flat list of days, so the frontend does a small calculation to place each day on the grid.

**Example: October** (month index 1)

1. **Find the month's start.** October starts at offset `30`, the sum of the lengths of the earlier months (September has 30 days). It covers offsets `30–60`.
2. **Draw the grid.** October has 31 days, and `FirstDay = 2`, so day 1 sits under Tuesday.
3. **Place the days.** Take the `Days` entries with offsets `30–60`. The day of the month is `Offset − 30 + 1`, so offset `37` is **Oct 8**.
4. **Handle empty days.** A day with no entry shows "0 positions".

### Good to know

- **Monday/Sunday toggle:** this only changes which column comes first. It needs no data from the server.
- **"Calendar Data" toggle:** this data is exposed separately and flat, with no unnecessary nesting (`ProfitUsdt`, `ProfitPercent`).

## Alternative: nest days inside each month

If you'd rather the frontend didn't add up month lengths, `Days` can be nested inside each `Month`. Then `Offset` is simply the day of the month (`8` for Oct 8).

I wouldn't recommend this, because nesting makes lookups more complex.

## Trade-offs of the flat structure

| Pros | Cons |
| --- | --- |
| The response holds only real data. Days with 0 positions are omitted, so there are no explicit nulls. | The response can get large, depending on how many positions the user has in the period. |
| Lookup complexity is acceptable for the frontend. | |

**Mitigating response size:** add query parameters for the target year and month. Alternatively, enable response compression.
