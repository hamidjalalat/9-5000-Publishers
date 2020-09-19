var app = new Vue({

    el: "#app",
    data: {
     
        pageBaseFullLists: null,
        postion: 0,
        hasClassGoldenTipsSuccess: false,
        hasClassGoldenTipsInfo: true,
        hasClassConceptualPointsSuccess: false,
        hasClassConceptualPointsInfo: true,
        hasClassLetterChartsSuccess: false,
        hasClassLetterChartsInfo: true,
        hasClassLetterTablesSuccess: false,
        hasClassLetterTablesInfo: true,
        hasClassMoviesSuccess: false,
        hasClassMoviesTablesInfo: true,
        pagebase: null,
        pagenumber: "1",
        countPage: 0,
        basehref: "",
        tipshref: null,
        idTips: 1,
     

    },
    methods: {
        goldenTips() {
            this.idTips = 1;
            this.tipshref = "/ShowContantBook/TipsEpub/?pageNumber=" + this.pagenumber + "&idTips=" + this.idTips;
            this.hasClassGoldenTipsSuccess = true;
            this.hasClassGoldenTipsInfo = false;
            this.hasClassConceptualPointsInfo = true;
            this.hasClassConceptualPointsSuccess = false;
            this.hasClassLetterChartsInfo = true;
            this.hasClassLetterChartsSuccess = false;
            this.hasClassLetterTablesInfo = true;
            this.hasClassLetterTablesSuccess = false;
            this.hasClassMoviesTablesInfo = true;
            this.hasClassMoviesSuccess = false;
            $("#goldenTips").show();
            $('#conceptualPoints').hide();
            $('#letterCharts').hide();
            $('#letterTables').hide();
            $('#movies').hide();


            $("#goldenTips").slideDown("slow");

        },
        conceptualPoints() {
            this.idTips = 2;
            this.tipshref = "/ShowContantBook/TipsEpub/?pageNumber=" + this.pagenumber + "&idTips=" + this.idTips;
            this.hasClassGoldenTipsSuccess = false;
            this.hasClassGoldenTipsInfo = true;
            this.hasClassConceptualPointsInfo = false;
            this.hasClassConceptualPointsSuccess = true;
            this.hasClassLetterChartsInfo = true;
            this.hasClassLetterChartsSuccess = false;
            this.hasClassLetterTablesInfo = true;
            this.hasClassLetterTablesSuccess = false;
            this.hasClassMoviesTablesInfo = true;
            this.hasClassMoviesSuccess = false;
            $('#letterCharts').hide();
            $('#letterTables').hide();
            $('#movies').hide();
            $("#goldenTips").hide();
            $("#conceptualPoints").slideDown("slow");
        },
        letterCharts() {
            this.idTips = 3;
            this.tipshref = "/ShowContantBook/TipsEpub/?pageNumber=" + this.pagenumber + "&idTips=" + this.idTips;
            this.hasClassGoldenTipsSuccess = false;
            this.hasClassGoldenTipsInfo = true;
            this.hasClassConceptualPointsInfo = true;
            this.hasClassConceptualPointsSuccess = false;
            this.hasClassLetterChartsInfo = false;
            this.hasClassLetterChartsSuccess = true;
            this.hasClassLetterTablesInfo = true;
            this.hasClassLetterTablesSuccess = false;
            this.hasClassMoviesTablesInfo = true;
            this.hasClassMoviesSuccess = false;

            $('#conceptualPoints').hide();
            $('#letterTables').hide();
            $('#movies').hide();
            $("#goldenTips").hide();
            $("#letterCharts").slideDown("slow");
        },
        letterTables() {
            this.idTips = 4;
            this.tipshref = "/ShowContantBook/TipsEpub/?pageNumber=" + this.pagenumber + "&idTips=" + this.idTips;
            this.hasClassGoldenTipsSuccess = false;
            this.hasClassGoldenTipsInfo = true;
            this.hasClassConceptualPointsInfo = true;
            this.hasClassConceptualPointsSuccess = false;
            this.hasClassLetterChartsInfo = true;
            this.hasClassLetterChartsSuccess = false;
            this.hasClassLetterTablesInfo = false;
            this.hasClassLetterTablesSuccess = true;
            this.hasClassMoviesTablesInfo = true;
            this.hasClassMoviesSuccess = false;
            $('#conceptualPoints').hide();
            $('#letterCharts').hide();
            $('#movies').hide();
            $("#goldenTips").hide();
            $("#letterTables").slideDown("slow");
        },
        movies() {
            this.hasClassGoldenTipsSuccess = false;
            this.hasClassGoldenTipsInfo = true;
            this.hasClassConceptualPointsInfo = true;
            this.hasClassConceptualPointsSuccess = false;
            this.hasClassLetterChartsInfo = true;
            this.hasClassLetterChartsSuccess = false;
            this.hasClassLetterTablesInfo = true;
            this.hasClassLetterTablesSuccess = false;
            this.hasClassMoviesTablesInfo = false;
            this.hasClassMoviesSuccess = true;
            $('#conceptualPoints').hide();
            $('#letterCharts').hide();
            $('#letterTables').hide();
            $("#goldenTips").hide();
            $("#movies").slideDown("slow");
        },
        serche() {
         
            this.postion = this.pagenumber -1;
            this.pagebase = this.pageBaseFullLists[this.postion].serverPath;
            this.basehref = "/ShowContantBook/PageBaseEpub/?path=" + this.pagebase;
           

        },
       
        first() {
            this.postion = 0;
            this.pagebase = this.pageBaseFullLists[this.postion].serverPath;
            this.pagenumber = this.pageBaseFullLists[this.postion].pageNumber;
            this.basehref = "/ShowContantBook/PageBaseEpub/?path=" + this.pagebase;
        },
        next() {

            if (this.postion == this.pageBaseFullLists.length - 1) {
                this.pagenumber = this.pageBaseFullLists[this.postion].pageNumber;
                return;
            }
            else {
                this.postion++;
                this.pagenumber = this.pageBaseFullLists[this.postion].pageNumber;

            }
            this.pagebase = this.pageBaseFullLists[this.postion].serverPath;
            this.pagenumber = this.pageBaseFullLists[this.postion].pageNumber;
            this.basehref = "/ShowContantBook/PageBaseEpub/?path=" + this.pagebase;
        },
        previous() {
            if (this.postion == 0) {
                return;
            }
            else {
                this.postion--;
                this.pagenumber = this.pageBaseFullLists[this.postion].pageNumber;
            }
            this.pagenumber = this.pageBaseFullLists[this.postion].pageNumber;
            this.pagebase = this.pageBaseFullLists[this.postion].serverPath;
            this.basehref = "/ShowContantBook/PageBaseEpub/?path=" + this.pagebase;

        },
        last() {
            this.pagenumber = this.pageBaseFullLists[this.pageBaseFullLists.length - 1].pageNumber;
            this.pagebase = this.pageBaseFullLists[this.pageBaseFullLists.length - 1].serverPath;
            this.postion = this.pageBaseFullLists.length - 1;
            this.basehref = "/ShowContantBook/PageBaseEpub/?path=" + this.pagebase;
        }
    },
    watch: {
        pagebase: function (val) {
            this.tipshref = "/ShowContantBook/TipsEpub/?pageNumber=" + this.pagenumber + "&idTips=" + this.idTips;
        },
    },
    computed: {
    },
    created: function () {
        $('#conceptualPoints').hide();
        $('#letterCharts').hide();
        $('#letterTables').hide();
        $('#movies').hide();
        $('#goldenTips').hide();

    },
    mounted() {
       
        axios.post('/ShowContantBook/PageBaseFullList')
            .then(response => {
                this.pageBaseFullLists = response.data.pageBaseFullLists;
                console.log(this.pageBaseFullLists);
                this.pagebase = this.pageBaseFullLists[0].serverPath;
                this.countPage = this.pageBaseFullLists.length;
                this.basehref = "/ShowContantBook/PageBaseEpub/?path=" + this.pagebase;
                this.tipshref = "/ShowContantBook/tipsEpub/?pageNumber=1&idTips=1";
              
            })
    },
})