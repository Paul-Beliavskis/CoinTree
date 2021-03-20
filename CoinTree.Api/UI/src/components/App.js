import '../styles/App.css';
import {getCoinStatsAsync} from '../services/CoinApi';
import {useState, useEffect} from 'react';
import { config } from '@fortawesome/fontawesome-svg-core'
import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import MenuItem from '@material-ui/core/MenuItem';
import FormHelperText from '@material-ui/core/FormHelperText';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import InputLabel from '@material-ui/core/InputLabel';

const useStyles = makeStyles({
  table: {
    minWidth: 650,
  },
});

function createData(ask, bid, spotRate, change) {
  return { ask, bid, spotRate, change };
}

function CalculatePercentChange(previous, current) {
  return  100 * Math.abs( ( previous - current ) / ( (previous + current)/2 ) );
 }

function App() {
  const [coinStat, setCoinStat] = useState({});
  const classes = useStyles();
  const [spotRates, setSpotRates] = useState({btc: 0, eth: 0, xpr: 0});

  useEffect(() => {
    fetchData('BTC')
  }, [])

  const rows = [
    createData(coinStat.ask, coinStat.bid, coinStat.spotRate, coinStat.change),
  ];

  const fetchData = (coin) => {
    getCoinStatsAsync(coin).then((response) => {
      if(response.status === 200)
      {
        let coinName = coin.toLowerCase()

        var stat = spotRates[coinName]
        var percentChange = 0
        if(stat !== 0)
        {
           percentChange = CalculatePercentChange(stat, response.data.spotRate)
        }

        spotRates[coinName] = response.data.spotRate;
        setSpotRates({...spotRates})

        setCoinStat({...response.data, ...{change: percentChange}});
      }
    });
  }

  return (
    <div className="App">
          <FormControl  className={classes.formControl} style={{width:'5%'}}>
        <Select
          defaultValue={'BTC'}
          style={{textAlign:'center', backgroundColor:'white', marginRight: '20px'}}
          onChange={(e) => fetchData(e.target.value)}
        >
          <MenuItem value={'BTC'}>BTC</MenuItem>
          <MenuItem value={'ETH'}>ETH</MenuItem>
          <MenuItem value={'XRP'}>XRP</MenuItem>
        </Select>
      </FormControl>
    <TableContainer style={{width: '35%'}} component={Paper}>
      <Table className={classes.table} size="small" aria-label="a dense table">
        <TableHead>
          <TableRow>
            <TableCell>Ask (AUD)</TableCell>
            <TableCell align="right">Bid (AUD)</TableCell>
            <TableCell align="right">Spot rate (AUD)</TableCell>
            <TableCell align="right">Change</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {rows.map((row, index) => (
            <TableRow key={index}>
              <TableCell align="left">{row.ask}</TableCell>
              <TableCell align="right">{row.bid}</TableCell>
              <TableCell align="right">{row.spotRate}</TableCell>
              <TableCell align="right" style={{color: row.change > -1 ? 'green' : 'red'}}>{row.change} %</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
    </div>
  );
}

export default App;