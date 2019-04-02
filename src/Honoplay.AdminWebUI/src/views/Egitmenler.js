import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid, Typography, Card, CardContent  } from '@material-ui/core';
import ButtonComponent from '../components/Button/ButtonComponent';
import InputTextComponent from '../components/InputText/InputTextComponent';
import SearchInputComponent from '../components/InputText/SearchInputComponent';
import TableComponent from '../components/Table/TableComponent';
import NumberInputComponent from '../components/InputText/NumberInputComponent';
import DropDownInputComponent from '../components/InputText/DropDownInputComponent';
import ChipComponent from '../components/Cards/ChipComponent';

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

class Egitmenler extends React.Component {

constructor(props) {
    super(props);
    this.state= {
        departmanData: [
            { key: 1, label: 'Yazılım',  },
            { key: 2, label: 'İnsan Kaynakları',  },
            { key: 3, label: 'Muhasebe',  },
            ],
        alanData: [
            { key: 1, label: 'Web Tasarım',  },
            { key: 2, label: 'Ticaret Hukuk',  },
            { key: 3, label: 'Yazılım',  },
            { key: 4, label: 'Siber Güvenlik',  },
            { key: 5, label: 'İnsan Kaynakları',  },
            ]};
}
      
render() {
  const { classes } = this.props;

  const columns = ['Ad', 'Soyad', 'E-Posta', 'Cep Telefon', 'Departman', 'Uzmanlık Alanları'];
  const data = [{ 'Ad':'Alper',
                  'Soyad': 'Halıcı',
                  'E-Posta': 'alper@2024arena.com',
                  'Cep Telefon': '(555)-555-5555',
                  'Departman': 'Tasarım Dep.',
                  'Uzmanlık Alanları': 'Web Tasarım, Ticaret Hukuku'
                }];
  
    return (
        <div className={classes.root}>
        <Grid container spacing={24}>
            <Grid item sm={9}>
                <Typography variant="h5" className={classes.Typography} >
                Eğitmenler
                </Typography>
            </Grid>
            <Grid item sm={3}>
                <ButtonComponent ButtonColor="primary" 
                                ButtonIcon="file-excel" 
                                ButtonName="Excel'den Aktar"/>          
            </Grid>
            <Grid item sm={12}>
            <InputTextComponent LabelName="Eğitmen Adı" 
                                InputId="inputEgitmenAd" 
                                InputType="text"/>
            <InputTextComponent LabelName="Eğitmen Soyadı" 
                                InputId="inputEgitmenSoyad" 
                                InputType="text"/>
            <InputTextComponent LabelName="Eğitmen E-Posta" 
                                InputId="inputEgitmenEmail" 
                                InputType="email"/>
            <NumberInputComponent LabelName="Cep Telefonu" 
                                    InputId="inputEgitmenTel" />
            <DropDownInputComponent data={this.state.departmanData}
                                    LabelName="Departman"/>
            </Grid>
            <Grid item xs={9}>
                <InputTextComponent LabelName="Eğitmen Uzmanlık Alanları" 
                                    InputId="inputEgitmenAlan" 
                                    InputType="text"/>
            </Grid>
            <Grid item xs={3}>
                <ButtonComponent ButtonColor="secondary" 
                                ButtonName="Uzmanlık Alanı Ekle"/>     
            </Grid>
            <Grid item xs={12}>
                <ChipComponent data={this.state.alanData} 
                               CardName=" Eğitmen Uzmanlık Alanları">
                </ChipComponent>
            </Grid>
            <Grid item sm={9}></Grid>
            <Grid item sm={3}>
            <ButtonComponent ButtonColor="secondary"  
                            ButtonName="Eğitmeni Kaydet"/>
            </Grid>
            <Grid item xs={12}>
            <Card className={classes.card}>
                <CardContent>
                <Grid item xs={12}>
                    <SearchInputComponent InputId="inputEgitmenAra"
                                        PlaceHolderName="Eğitmen Ara"/>
                </Grid>
                <Grid item xs={12}>
                    <TableComponent columns={columns} 
                                    data={data}
                    />
                </Grid>
                </CardContent>
            </Card>
            </Grid>
        </Grid>
        </div>
        );
    }
}
Egitmenler.propTypes = {
  classes: PropTypes.object.isRequired,
};
export default withStyles(styles)(Egitmenler);