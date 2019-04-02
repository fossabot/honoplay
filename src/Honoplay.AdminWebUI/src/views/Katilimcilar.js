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

class Katilimcilar extends React.Component {
 
  constructor(props) {
    super(props);
    this.state= {
      data: [
        { key: 1, label: 'Katılımcı Grubu 1', date: '01.02.2019' },
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
            <CardButton CardName="Katılımcı Grubu Oluştur"
                        CardDescription="Eğitim serileri oluşturup farklı farklı 
                        eğitimleri bir alanda toplayabilirsiniz."
            />
        </Grid>
        <Grid item xs={12} sm={9}>
            <CardComponent data={this.state.data}/>
        </Grid>
      </Grid>
    </div>
  );
  }
}
Katilimcilar.propTypes = {
  classes: PropTypes.object.isRequired,
};
export default withStyles(styles)(Katilimcilar);


