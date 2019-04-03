import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid, Typography, Card, CardContent, Divider, InputLabel} from '@material-ui/core';
import InputTextComponent from '../components/InputText/InputTextComponent';
import SearchInputComponent from '../components/InputText/SearchInputComponent';
import ButtonComponent from '../components/Button/ButtonComponent';
import TableComponent from '../components/Table/TableComponent';
import DropDownInputComponent from '../components/InputText/DropDownInputComponent';
import ChipComponent from '../components/Cards/ChipComponent';
import NumberInputComponent from '../components/InputText/NumberInputComponent';
import FileInput from '../components/InputText/FileInputComponent';
import StepperComponent from '../components/Stepper/StepperComponent';

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
  button: {
    margin: theme.spacing.unit,
  },
  input: {
    display: 'none',
  },
});

class SirketBilgileri extends React.Component {

constructor(props) {
    super(props);
    this.state= {
        calismaDurumu: [
            { key: 1, label: 'Çalışan',  },
            { key: 2, label: 'Aday',  },
            { key: 3, label: 'Stajyer',  },
            ],
        sirketdepartmanData: [
            { key: 1, label: 'Tasarım',  },
            { key: 2, label: 'Yazılım',  },
            { key: 3, label: 'Yönetim',  },
            { key: 4, label: 'Satış Pazarlama',  },
            { key: 5, label: 'İnsan Kaynakları',  },
            ],
        kisidepartmanData: [
            { key: 1, label: 'Tasarım', },
            ],
        cinsiyet: [
            { key: 1, label: 'Erkek', },
            { key: 1, label: 'Kadın', },
            ],
        };
}


render() {
  const { classes } = this.props;

  const columns = ['Durum','Ad','Soyad',  'Departman', 'Cep Telefon', 'Cinsiyet'];
  const data = [{ 'Durum':'Çalışan',
                  'Ad': 'Alper',
                  'Soyad': 'Halıcı',
                  'Departman': 'Pazarlama',
                  'Cep Telefon': '(555)-555-5555',
                  'Cinsiyet': 'Erkek'
                }];
  
    return (
        <div className={classes.root}>
        <Grid container spacing={24}>
            <Grid item sm={12}>
                <Typography variant="h5" className={classes.Typography} >
                Şirket Bilgileri
                </Typography>
            </Grid>
            <Grid item sm={12} xs={12} >
            <InputTextComponent LabelName="Şirket Adı" 
                                InputId="inputSirketAd" 
                                InputType="text"/>
            </Grid>
            <Grid item xs={12}>
                <FileInput LabelName="Şirket Logosu"/>
            </Grid>
            <Grid item xs={12} sm={8}>
                <InputTextComponent LabelName="Departman" 
                                    InputId="inputdepartman" 
                                    InputType="text"/>
            </Grid>
            <Grid item xs={3} sm={1}>
                <ButtonComponent ButtonColor="secondary" 
                                 ButtonName="Ekle"/>     
            </Grid>
            <Grid item xs={9} sm={3}>
                <ButtonComponent ButtonColor="primary" 
                                    ButtonIcon="file-excel" 
                                    ButtonName="Excel'den Aktar"/>     
            </Grid>
            <Grid item xs={12}>
                <ChipComponent data={this.state.sirketdepartmanData}
                               CardName="Şirket Departmanları">
                </ChipComponent>
            </Grid>
            <Grid item xs={12}>
             <Divider/>
            </Grid>
            <Grid item sm={9}>
                <Typography variant="h5" className={classes.Typography} >
                Kişi Ekle
                </Typography>
            </Grid>
            <Grid item sm={3}>
                <ButtonComponent ButtonColor="primary" 
                                ButtonIcon="file-excel" 
                                ButtonName="Excel'den Aktar"/>          
            </Grid>
            <Grid item xs={12}>
                <DropDownInputComponent data={this.state.calismaDurumu}
                                        LabelName="Çalışma Durumu"/>
                <InputTextComponent LabelName="Ad" 
                                    InputId="inputKisiAd" 
                                    InputType="text"/>
                <InputTextComponent LabelName="Soyad" 
                                    InputId="inputKisiSoyad" 
                                    InputType="text"/>
                <DropDownInputComponent data={this.state.kisidepartmanData}
                                        LabelName="Departman"/>
                <InputTextComponent LabelName="TCKN" 
                                    InputId="inputTCKN" 
                                    InputType="text"/>
                <NumberInputComponent LabelName="Cep Telefonu" 
                                      InputId="inputTel" />
                <DropDownInputComponent data={this.state.cinsiyet}
                                        LabelName="Cinsiyet"/>
            </Grid>
            <Grid item sm={10}></Grid>
            <Grid item sm={2}>
            <ButtonComponent ButtonColor="secondary"  
                             ButtonName="Kişiyi Ekle"/>
            </Grid>
            <Grid item xs={12}>
            <Card className={classes.card}>
                <CardContent>
                <Grid item xs={12}>
                    <SearchInputComponent InputId="inputKatilimciAra"
                                        PlaceHolderName="Katılımcı Ara"/>
                </Grid>
                <Grid item xs={12}>
                    <TableComponent columns={columns} 
                                    data={data}
                    />
                </Grid>
                </CardContent>
            </Card>
            </Grid>
            <Grid item xs={6} sm={3}>
                <ButtonComponent ButtonColor="primary" 
                                ButtonName="Bilgileri Güncelle"/>  
            </Grid>           
            <Grid item xs={6} sm={3}>  
                <ButtonComponent ButtonColor="secondary" 
                                 ButtonName="Şirketi Kaydet"
                />  
            </Grid>
        </Grid>
        </div>
        );
    }
}
SirketBilgileri.propTypes = {
  classes: PropTypes.object.isRequired,
};
export default withStyles(styles)(SirketBilgileri);