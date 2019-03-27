import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid, Typography } from '@material-ui/core';
import CardButton from '../components/Cards/CardButton';
import CardComponent from '../components/Cards/CardComponent';

const styles = theme => ({
  root: {
    flexGrow: 1,
  },
  Typography: {
    margin: theme.spacing.unit,
    color: '#e92428',
    fontWeight: 'bold',
  },
  card: {
    minWidth: 275,
   
  },
});

class EgitimSerisi extends React.Component {
 
  constructor(props) {
    super(props);
    this.state= {
      data: [
        { key: 1, label: 'Eğitim Serisi 1', date: '01.02.2019' },
        { key: 2, label: 'Eğitim Serisi 2', date: '03.02.2019' },
        { key: 3, label: 'Eğitim Serisi 3', date: '01.05.2019' },
      ]};
  }
  
  render() {
    const { classes } = this.props;
  return (
    <div className={classes.root}>
      <Grid container spacing={24}>
        <Grid item xs={12}>
            <Typography variant="h5" className={classes.Typography}>
                Eğitim Serisi
            </Typography>
        </Grid>
        <Grid item xs={12} sm={3}>
            <CardButton CardName="Eğitim Serisi Oluştur"
                        CardDescription="Eğitim serileri oluşturup farklı farklı 
                        eğitimleri bir alanda toplayabilirsiniz."
            />
        </Grid>
        <Grid item xs={12} sm={9}>
            <CardComponent
                           data={this.state.data}
            />
        </Grid>
      </Grid>
    </div>
  );
  }
}
EgitimSerisi.propTypes = {
  classes: PropTypes.object.isRequired,
};
export default withStyles(styles)(EgitimSerisi);


